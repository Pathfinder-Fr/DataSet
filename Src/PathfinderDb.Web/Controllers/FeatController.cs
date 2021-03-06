﻿// -----------------------------------------------------------------------
// <copyright file="FeatController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Datas;
    using Models;
    using Models.Feat;
    using Schema;

    public class FeatController : Controller
    {
        public ActionResult Index(IndexViewModel model)
        {
            using (var db = this.OpenDb())
            {
                //model.Feats = db.Documents.Where(d => d.Type == DbDocumentType.Feat).ToList().Select(f => f.As<Feat>());
            }

            return View(model);
        }

        public ActionResult New(EditViewModel model)
        {
            return View("Edit", model);
        }
    }
}