// -----------------------------------------------------------------------
// <copyright file="GearController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Datas;
    using Models.Gear;
    using Schema;
    using Store;

    public class GearController : Controller
    {
        private readonly IDocumentService docService;

        public GearController(IDocumentService docService)
        {
            this.docService = docService;
        }

        public ActionResult Index(IndexViewModel model)
        {
            using (var db = this.docService.OpenSession())
            {
                model.Items = db.LoadGears(model).Select(x => x.AsItem()).ToList();
            }

            return View(model);
        }

        public ActionResult New()
        {
            return this.View(new EditViewModel());
        }

        [HttpPost]
        public ActionResult New(EditViewModel model)
        {
            return this.Save(model, true);
        }

        public ActionResult Edit(int id)
        {
            using (var db = this.docService.OpenSession())
            {
                var doc = db.LoadGear(id);
                return this.ViewOrNotFound(doc.AsEdit());
            }
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            using (var db = this.docService.OpenSession())
            {
                // charge
                var doc = db.LoadGear(model.DocId);
                
                // modifie
                doc.Apply(model);

                // enregistre
                db.SaveGear(doc);
            }
            return this.Save(model, false);
        }

        public ActionResult Delete(string lang, string id)
        {
            this.ViewBag.AlertSuccess = @"L'équipement a bien été supprimé";
            return this.RedirectToAction("Index");
        }
    }
}