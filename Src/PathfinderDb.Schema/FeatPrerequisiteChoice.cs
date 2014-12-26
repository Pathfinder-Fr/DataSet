// -----------------------------------------------------------------------
// <copyright file="FeatPrerequisiteChoice.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("choice")]
    public class FeatPrerequisiteChoice : IFeatPrerequisiteItem
    {
        [XmlElement("prerequisite")]
        public FeatPrerequisite[] Items { get; set; }
    }
}