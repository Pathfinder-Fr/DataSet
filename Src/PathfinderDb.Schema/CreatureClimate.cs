﻿// -----------------------------------------------------------------------
// <copyright file="CreatureClimate.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("creatureClimate")]
    public enum CreatureClimate
    {
        [XmlEnum("")] Other,

        [XmlEnum("planar")] Planar,

        [XmlEnum("cold")] Cold,

        [XmlEnum("temperate")] Temperate,

        [XmlEnum("warm")] Warm,
    }
}