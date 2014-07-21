using Microsoft.AspNet.Mvc;
using PathfinderDb.Models;
using System;

namespace PathfinderDb
{
    /// <summary>
    /// Summary description for HomeController
    /// </summary>
    public class HomeController : Controller
    {
        private readonly PathfinderDbContext context;

	    public HomeController(PathfinderDbContext context)
	    {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}