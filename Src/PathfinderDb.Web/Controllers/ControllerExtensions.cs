// -----------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Web;
using System.Web.Mvc;
using PathfinderDb.Datas;
using Raven.Client;
using Raven.Client.Embedded;

namespace PathfinderDb.Controllers
{
    internal static class ControllerExtensions
    {
        private static EmbeddableDocumentStore store;

        public static IDocumentSession OpenSession(this Controller @this)
        {
            if (store == null)
            {
                store = new EmbeddableDocumentStore
                {
                    DataDirectory = string.Format(@"{0}\RavenDB", HttpContext.Current.Server.MapPath("~/App_Data"))
                };

                store.Initialize();
            }

            return store.OpenSession();
        }

        public static PathfinderDbContext OpenDb(this Controller @this)
        {
            return PathfinderDbContext.Create();
        }

        public static ActionResult ViewOrNotFound<T>(this Controller @this, T viewModel)
        {
            if (Equals(viewModel, default(T)))
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