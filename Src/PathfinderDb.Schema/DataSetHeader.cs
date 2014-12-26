// -----------------------------------------------------------------------
// <copyright file="DataSetHeader.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("header")]
    public class DataSetHeader
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("generator")]
        public string Generator { get; set; }
    }
}