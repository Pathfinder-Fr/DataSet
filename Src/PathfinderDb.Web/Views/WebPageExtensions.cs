// -----------------------------------------------------------------------
// <copyright file="WebPageExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Views
{
    using System.Web.WebPages;

    public static class WebPageExtensions
    {
        public static string T(this WebPageBase @this, string name, params object[] args)
        {
            return string.Format(Properties.Resources.ResourceManager.GetString(name) ?? name, args);
        }
    }
}