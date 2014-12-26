﻿// -----------------------------------------------------------------------
// <copyright file="WeightUnit.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    /// <summary>
    /// Specifies the unit used to describe a weight.
    /// </summary>
    [XmlType("weightUnit")]
    public enum WeightUnit
    {
        /// <summary>
        /// Pounds.
        /// </summary>
        [XmlEnum("lbs")] Pounds,

        /// <summary>
        /// Kilograms.
        /// </summary>
        [XmlEnum("kg")] Kilogram,
    }
}