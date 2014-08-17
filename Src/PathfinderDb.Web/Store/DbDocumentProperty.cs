// -----------------------------------------------------------------------
// <copyright file="DbDocumentProperty.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Datas
{
    public class DbDocumentProperty
    {
        public int Id { get; set; }

        public DbDocumentType DocType { get; set; }

        public DbDocumentPropertyType Type { get; set; }

        public string Name { get; set; }
    }
}