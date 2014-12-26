// -----------------------------------------------------------------------
// <copyright file="SpellListLevel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("spellListLevel")]
    public class SpellListLevel
    {
        [XmlAttribute("list")]
        public string List { get; set; }

        [XmlAttribute("level")]
        public int Level { get; set; }
    }
}