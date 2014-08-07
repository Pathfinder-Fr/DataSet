// -----------------------------------------------------------------------
// <copyright file="GearController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Web.Mvc;
    using Models;
    using Schema;
    using ViewModels;

    public class GearController : ItemController<Gear, GearItemViewModel, GearEditViewModel>
    {
        public GearController() : base(DbDocumentType.Gear)
        {
        }

        public ActionResult Index(Page<GearItemViewModel> model)
        {
            using (var db = this.OpenDb())
            {
                model.Items = this.LoadItems(db);
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
            return this.Save(model, true);
        }

        public ActionResult Edit(int id)
        {
            return this.ViewOrNotFound(this.LoadEdit(id));
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
    }
}