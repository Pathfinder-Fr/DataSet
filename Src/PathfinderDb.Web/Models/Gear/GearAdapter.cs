// -----------------------------------------------------------------------
// <copyright file="GearAdapter.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using System;
    using Datas;
    using Schema;

    public class GearAdapter : ISchemaAdapter
    {
        public static readonly ISchemaAdapter Instance = new GearAdapter();

        public DbDocumentType DocType
        {
            get { return DbDocumentType.Spells; }
        }

        public Type SchemaType
        {
            get { return typeof(Gear); }
        }

        public string GetName(Element schema)
        {
            return ((Gear)schema).Name;
        }

        public string GetCategory(Element schema)
        {
            return ((Gear)schema).Category.ToString();
        }

        public string GetNameProperty()
        {
            return "name";
        }
    }
}