// -----------------------------------------------------------------------
// <copyright file="SpellRangeUnit.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("spellRangeUnit")]
    public enum SpellRangeUnit
    {
        [XmlEnum("specific")] Specific = 0,

        [XmlEnum("personal")] Personal,

        [XmlEnum("touch")] Touch,

        [XmlEnum("close")] Close,

        [XmlEnum("medium")] Medium,

        [XmlEnum("long")] Long,

        [XmlEnum("unlimited")] Unlimited,

        [XmlEnum("squares")] Squares,
    }
}