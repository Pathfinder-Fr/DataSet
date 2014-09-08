// -----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();

            using(var db = this.OpenDb())
            {
                //var groupCount = db.Documents.GroupBy(d => d.Type).Select(d => new { Type = d.Key, Count = d.Count() }).ToDictionary(x => x.Type, x => x.Count);

                //model.CreateGroups(groupCount);
            }

            return this.View(model);
        }

        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}