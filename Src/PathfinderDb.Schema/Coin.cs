// -----------------------------------------------------------------------
// <copyright file="Coin.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("coin")]
    public enum Coin
    {
        [XmlEnum("cp")] Copper = 0,

        [XmlEnum("sp")] Silver = 1,

        [XmlEnum("gp")] Gold = 2,

        [XmlEnum("pp")] Platinium = 3
    }
}