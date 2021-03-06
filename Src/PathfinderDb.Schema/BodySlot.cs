﻿// -----------------------------------------------------------------------
// <copyright file="BodySlot.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("bodySlot")]
    public enum BodySlot
    {
        [XmlEnum("none")] None,

        [XmlEnum("armor")] Armor,

        [XmlEnum("belts")] Belts,

        [XmlEnum("chest")] Chest,

        [XmlEnum("eyes")] Eyes,

        [XmlEnum("feet")] Feet,

        [XmlEnum("hands")] Hands,

        [XmlEnum("head")] Head,

        [XmlEnum("headband")] Headband,

        [XmlEnum("neck")] Neck,

        [XmlEnum("ring")] Ring,

        [XmlEnum("shield")] Shield,

        [XmlEnum("shoulders")] Shoulders,

        [XmlEnum("wrist")] Wrist
    }
}