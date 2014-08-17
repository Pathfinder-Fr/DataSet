// -----------------------------------------------------------------------
// <copyright file="SpellAdapter.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Spell
{
    using System;
    using Datas;
    using Schema;

    public class SpellAdapter : ISchemaAdapter
    {
        public static readonly ISchemaAdapter Instance = new SpellAdapter();

        public DbDocumentType DocType
        {
            get { return DbDocumentType.Spells; }
        }

        public Type SchemaType
        {
            get { return typeof(Spell); }
        }

        public string GetName(Element schema)
        {
            return ((Spell)schema).Name;
        }

        public string GetCategory(Element schema)
        {
            return ((Spell)schema).School.ToString();
        }

        public string GetNameProperty()
        {
            return "name";
        }
    }
}