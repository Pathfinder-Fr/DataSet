// -----------------------------------------------------------------------
// <copyright file="FeatController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Schema;
    using Models;
    using ViewModels;

    public class FeatController : Controller
    {
        public ActionResult Index(FeatIndexViewModel model)
        {
            using (var db = this.OpenDb())
            {
                model.Feats = db.Documents.Where(d => d.Type == DbDocumentType.Feat).ToList().Select(f => f.As<Feat>());
            }

            return View(model);
        }

        public ActionResult New(FeatEditViewModel model)
        {
            return View("Edit", model);
        }
    }
}