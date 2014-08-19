using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathfinderDb.Store
{
    using Raven.Abstractions.Data;
    using Raven.Client.Embedded;

    public class Stores
    {
        public static void Test(string appData)
        {
            var store = new EmbeddableDocumentStore { DataDirectory = string.Format(@"{0}\RavenDB", appData) };
            store.OpenSession();
        }
    }
}