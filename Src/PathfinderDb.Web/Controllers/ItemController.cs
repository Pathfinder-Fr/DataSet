// -----------------------------------------------------------------------
// <copyright file="ItemController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Datas;
    using Models;
    using Schema;

    public abstract class ItemController<TSchema, TItem, TEdit> : Controller
        where TSchema : Element, IElementWithId
        where TItem : IItem<TSchema, TItem>, new()
        where TEdit : class, IEdit<TSchema, TEdit>, new()
    {
        private readonly DbDocumentType docType;

        protected ItemController(DbDocumentType docType)
        {
            this.docType = docType;
        }

        protected DbDocumentType DocType
        {
            get { return this.docType; }
        }

        protected List<TItem> LoadItems(PathfinderDbContext db, IEnumerable<Expression<Func<DbDocument, bool>>> predicates)
        {
            return LoadItems(db, predicates.ToArray());
        }

        protected List<TItem> LoadItems(PathfinderDbContext db, params Expression<Func<DbDocument, bool>>[] predicates)
        {
            var query = db
                .Documents
                .Where(d => d.Type == this.docType && d.Lang == DataSetLanguages.French);

            if (predicates != null)
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate);
                }
            }

            return query
                .ToList()
                .Select(ForItem)
                .ToList();
        }

        protected virtual TEdit LoadEdit(int id)
        {
            using (var db = this.OpenDb())
            {
                var dbDoc = db
                    .Documents
                    .FirstOrDefault(d => d.DocId == id && d.Type == this.docType);

                if (dbDoc == null)
                {
                    return default(TEdit);
                }

                var schema = dbDoc.As<TSchema>();
                var dbModel = ForEdit(schema);
                dbModel.DocId = dbDoc.DocId;
                return dbModel;
            }
        }

        protected virtual ActionResult Save(TEdit model, bool isNew)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            using (var db = this.OpenDb())
            {
                if (isNew)
                {
                    this.SaveNew(db, model);
                }
                else
                {
                    var result = this.SaveExisting(db, model);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            this.ViewBag.AlertSuccess = @"L'équipement a bien été enregistré";

            switch (model.SubmitAction)
            {
                case ViewSubmitAction.SaveAndBack:
                    return this.RedirectToAction("Index");

                case ViewSubmitAction.SaveAndNew:
                    this.ModelState.Clear();
                    return this.View(model.AsNew());

                default:
                    if (isNew)
                    {
                        // On passe en modification
                        return this.RedirectToAction("Edit", new { id = model.DocId });
                    }

                    // On reste sur le formulaire
                    return this.View(model);
            }
        }

        protected virtual void SaveNew(PathfinderDbContext db, TEdit model)
        {
            // TODO Vérifier qu'il n'existe pas déjà un objet avec ce lang/id
            var dbDoc = DbDocument.From(this.docType, model.Save(null));
            db.Documents.Add(dbDoc);

            db.SaveChanges();

            model.DocId = dbDoc.DocId;
        }

        protected virtual ActionResult SaveExisting(PathfinderDbContext db, TEdit model)
        {
            // Load existing document
            var dbDoc = db.Documents.FirstOrDefault(d => d.DocId == model.DocId);
            if (dbDoc == null)
            {
                this.ModelState.AddModelError("", @"L'élément indiqué n'existe plus. Les modifications n'ont pas pu être enregistrées");
                return this.View(model);
            }

            // Convert to edit it
            var schema = dbDoc.As<TSchema>();
            var dbModel = ForEdit(schema);
            dbModel.DocId = dbDoc.DocId;

            // Apply model changes
            if (!this.TryUpdateModel(dbModel))
            {
                return this.View(model);
            }

            // Save model to schema
            dbModel.Save(schema);

            // Update document
            dbDoc.SerializeContent(schema);
            dbDoc.Id = dbModel.Id;

            // Update database
            db.SaveChanges();

            return null;
        }

        private static TEdit ForEdit(TSchema schema)
        {
            return new TEdit().Load(schema);
        }

        private static TItem ForItem(DbDocument dbDoc)
        {
            var item = new TItem { DocId = dbDoc.DocId };
            item.Load(dbDoc.As<TSchema>());
            return item;
        }
    }
}