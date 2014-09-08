// -----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System;
    using System.Web.Mvc;
    using Schema;

    public class AdminController : Controller
    {
        public ActionResult Migrate()
        {
            using (var db = this.OpenDb())
            {
                //foreach (var dbDoc in db.Documents)
                //{
                //    try
                //    {
                //        var adapter = dbDoc.GetAdapter();
                //        var schema = (Element)dbDoc.As(adapter.SchemaType);
                //        dbDoc.SerializeContent(schema);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new InvalidOperationException(string.Format("Error when migrating {0}", dbDoc.Id), ex);

                //    }
                //}

                //db.SaveChanges();
            }

            return new EmptyResult();
        }
    }
}