// -----------------------------------------------------------------------
// <copyright file="Elements.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PathfinderDb.Schema
{
    public static class Elements
    {
        public static bool HasEnglishNameFor(this Element @this)
        {
            //var propertyName = adapter.GetNameProperty();

            //if (string.IsNullOrEmpty(propertyName))
            //{
            //    return false;
            //}

            throw new NotImplementedException();

            return @this.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, "") != null;
        }
    }
}