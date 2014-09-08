// -----------------------------------------------------------------------
// <copyright file="GearController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.Mvc;
using PathfinderDb.Models.Gear;
using Raven.Client;

namespace PathfinderDb.Controllers
{
    public class GearController : Controller
    {
        public ActionResult Index(IndexViewModel model)
        {
            using (IDocumentSession db = this.OpenSession())
            {
                IQueryable<GearDocument> query = db.Query<GearDocument>();

                query = model.AsExpressions().Aggregate(query, (current, expression) => current.Where(expression));

                model.Items = query.ToList().Select((x => x.AsItem())).ToList();
            }

            //using (var db = this.docService.OpenSession())
            //{
            //    model.Items = db.LoadGears(model).Select(x => x.AsItem()).ToList();
            //}

            return View(model);
        }

        public ActionResult New()
        {
            return View(new EditViewModel());
        }

        [HttpPost]
        public ActionResult New(EditViewModel model)
        {
            //return this.Save(model, true);
            return null;
        }

        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
            //    using (var db = this.docService.OpenSession())
            //    {
            //        var doc = db.LoadGear(id);
            //        return this.ViewOrNotFound(doc.AsEdit());
            //    }
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            //using (var db = this.docService.OpenSession())
            //{
            //    // charge
            //    var doc = db.LoadGear(model.DocId);

            //    // modifie
            //    doc.Apply(model);

            //    // enregistre
            //    db.SaveGear(doc);
            //}
            //return this.Save(model, false);
            return null;
        }

        public ActionResult Delete(string lang, string id)
        {
            ViewBag.AlertSuccess = @"L'équipement a bien été supprimé";
            return RedirectToAction("Index");
        }
    }
}