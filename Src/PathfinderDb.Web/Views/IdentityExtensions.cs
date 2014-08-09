// -----------------------------------------------------------------------
// <copyright file="IdentityExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Views
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using Microsoft.AspNet.Identity;

    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var ci = @this as ClaimsIdentity;

            if (ci != null)
            {
                return ci.FindFirstValue("DisplayName");
            }

            return null;
        }
    }
}