﻿// -----------------------------------------------------------------------
// <copyright file="EquipmentItem.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    [XmlType("equipmentItem", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class EquipmentItem : Element, IElementWithId
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("price")]
        public MoneyAmount Price { get; set; }

        [XmlElement("weight")]
        public WeightAmount Weight { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}