﻿// -----------------------------------------------------------------------
// <copyright file="SpellSavingThrowEffect.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("spellSavingThrowEffect")]
    public enum SpellSavingThrowEffect
    {
        [XmlEnum("none")] None = 0,

        [XmlEnum("negates")] Negates,

        [XmlEnum("partial")] Partial,

        [XmlEnum("half")] Half,

        [XmlEnum("disbelief")] Disbelief,

        [XmlEnum("special")] Special,
    }
}