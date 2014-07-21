using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathfinderDb.Controllers
{
    using System.Web.Mvc;
    using Models;

    internal static class ControllerExtensions
    {
        public static PathfinderDbContext OpenDb(this Controller @this)
        {
            return new PathfinderDbContext();
        }
    }
}