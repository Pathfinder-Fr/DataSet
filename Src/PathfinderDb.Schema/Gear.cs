// -----------------------------------------------------------------------
// <copyright file="Gear.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("gear", Namespace = Namespaces.PathfinderDb)]
    public class Gear : EquipmentItem
    {
        [XmlAttribute("category")]
        public GearCategory Category { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}