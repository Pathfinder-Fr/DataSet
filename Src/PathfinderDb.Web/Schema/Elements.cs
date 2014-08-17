// -----------------------------------------------------------------------
// <copyright file="Elements.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Elements
    {
        public static bool HasEnglishNameFor(this Element @this, Models.ISchemaAdapter adapter)
        {
            var propertyName = adapter.GetNameProperty();

            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            return @this.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, propertyName) != null;
        }
    }
}