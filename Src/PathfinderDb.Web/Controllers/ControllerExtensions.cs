// -----------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Web.Mvc;
    using Datas;
    using Models;

    internal static class ControllerExtensions
    {
        public static PathfinderDbContext OpenDb(this Controller @this)
        {
            return PathfinderDbContext.Create();
        }

        public static ActionResult ViewOrNotFound<T>(this Controller @this, T viewModel)
        {
            if(Equals(viewModel, default(T)))
            {
                return new HttpNotFoundResult(null);
            }

            @this.ViewData.Model = viewModel;

            return new ViewResult
            {
                ViewName = null,
                MasterName = null,
                ViewData = @this.ViewData,
                TempData = @this.TempData,
                ViewEngineCollection = @this.ViewEngineCollection
            };
        }
    }
}