// -----------------------------------------------------------------------
// <copyright file="MagicAuraStrength.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("magicAuraStrength")]
    public enum MagicAuraStrength
    {
        [XmlEnum("none")] None,

        [XmlEnum("faint")] Faint,

        [XmlEnum("moderate")] Moderate,

        [XmlEnum("strong")] Strong,

        [XmlEnum("overwhelming")] Overwhelming
    }
}