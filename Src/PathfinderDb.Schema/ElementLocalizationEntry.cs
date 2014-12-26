// -----------------------------------------------------------------------
// <copyright file="ElementLocalizationEntry.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("entry")]
    public class ElementLocalizationEntry
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}