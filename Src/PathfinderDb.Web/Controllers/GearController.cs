// -----------------------------------------------------------------------
// <copyright file="GearController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class GearController : Controller
    {
        public ActionResult Index(GearIndexViewModel model)
        {
            model.Items = new List<GearIndexItemViewModel>();
            return View(model);
        }

        public ActionResult New()
        {
            return this.View(new GearEditViewModel());
        }

        public ActionResult Edit(string id)
        {
            return this.View("New", new GearEditViewModel());
        }
    }
}