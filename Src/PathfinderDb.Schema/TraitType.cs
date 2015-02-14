// -----------------------------------------------------------------------
// <copyright file="TraitType.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    /// <summary>
    /// There are five types of character traits to choose from: basic (split among four categories: Combat, Faith, Magic, and Social), campaign, race, regional, and religion.
    /// </summary>
    [XmlType("traitType")]
    public enum TraitType
    {
        [XmlEnum("basic")] Basic = 0,

        [XmlEnum("campaign")] Campaign = 1,

        [XmlEnum("race")] Race = 2,

        [XmlEnum("regional")] Regional = 3,

        [XmlEnum("religion")] Religion = 4
    }
}