// -----------------------------------------------------------------------
// <copyright file="Elements.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Elements
    {
        public static string GetDocumentCategory(this Element @this, Models.DbDocumentType type)
        {
            switch (type)
            {
                case Models.DbDocumentType.Gear:
                    return ((Gear)@this).Category.ToString();

                default:
                    return null;
            }
        }

        public static bool HasEnglishNameFor(this Element @this, Models.DbDocumentType type)
        {
            var propertyName = GetNamePropertyFor(type);

            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            return @this.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, propertyName) != null;
        }

        private static string GetNamePropertyFor(Models.DbDocumentType type)
        {
            switch (type)
            {
                case Models.DbDocumentType.Gear:
                    return "name";

                default:
                    return null;
            }
        }
    }
}