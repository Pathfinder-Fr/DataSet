// -----------------------------------------------------------------------
// <copyright file="DataController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Web.Mvc;

    public class DataController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}