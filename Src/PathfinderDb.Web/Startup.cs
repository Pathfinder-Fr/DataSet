// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Owin;
using PathfinderDb;

[assembly: OwinStartup(typeof(Startup))]

namespace PathfinderDb
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}