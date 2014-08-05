// -----------------------------------------------------------------------
// <copyright file="GearController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Schema;
    using ViewModels;

    public class GearController : Controller
    {
        public ActionResult Index(GearIndexViewModel model)
        {
            using (var db = this.OpenDb())
            {
                model.Items = db.Documents
                    .Where(d => d.Type == Models.DbDocumentType.Gear && d.Lang == DataSetLanguages.French)
                    .ToList()
                    .Select(d => GearIndexItemViewModel.FromDataSet(d, d.As<Gear>()))
                    .ToList();
            }
            return View(model);
        }

        public ActionResult New()
        {
            return this.View(new GearEditViewModel());
        }

        [HttpPost]
        public ActionResult New(GearEditViewModel model)
        {
            return this.Save(model);
        }

        public ActionResult Edit(int id)
        {
            using (var db = this.OpenDb())
            {
                var dbDoc = db.Documents.FirstOrDefault(d => d.DocId == id && d.Type == Models.DbDocumentType.Gear);

                if (dbDoc == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(GearEditViewModel.FromSchema(dbDoc, dbDoc.As<Gear>()));
            }
        }

        [HttpPost]
        public ActionResult Edit(GearEditViewModel model)
        {
            return this.Save(model, false);
        }

        public ActionResult Delete(string lang, string id)
        {
            this.ViewBag.AlertSuccess = @"L'équipement a bien été supprimé";
            return this.RedirectToAction("Index");
        }

        private ActionResult Save(GearEditViewModel model, bool isNew = true)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            using (var db = this.OpenDb())
            {
                if (isNew)
                {
                    // TODO Vérifier qu'il n'existe pas déjà un objet avec ce lang/id
                    var dbDoc = Models.DbDocument.From(Models.DbDocumentType.Gear, model.AsSchema());
                    db.Documents.Add(dbDoc);

                    db.SaveChanges();

                    model.DocId = dbDoc.DocId;
                }
                else
                {
                    var dbDoc = db.Documents.FirstOrDefault(d => d.DocId == model.DocId);
                    if (dbDoc == null)
                    {
                        this.ModelState.AddModelError("", @"L'équipement indiqué n'existe plus. Les modifications n'ont pas pu être enregistrées");
                        return this.View(model);
                    }

                    var dbModel = GearEditViewModel.FromSchema(dbDoc, dbDoc.As<Gear>());

                    if (!this.TryUpdateModel(dbModel))
                    {
                        return this.View(model);
                    }

                    dbDoc.SerializeContent(dbModel.AsSchema());
                    dbDoc.Id = dbModel.Id;

                    db.SaveChanges();
                }
            }

            this.ViewBag.AlertSuccess = @"L'équipement a bien été enregistré";

            switch (model.SubmitAction)
            {
                case ViewSubmitAction.SaveAndBack:
                    return this.RedirectToAction("Index");

                case ViewSubmitAction.SaveAndNew:
                    this.ModelState.Clear();
                    return this.View(new GearEditViewModel { Category = model.Category, Source = model.Source });

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
    }
}