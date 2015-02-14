// -----------------------------------------------------------------------
// <copyright file="ElementSources.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class ElementSources
    {
        public static string ToDisplayString(this ElementSource @this)
        {
            return Sources.IdToDisplayString(@this.Id);
        }
    }
}