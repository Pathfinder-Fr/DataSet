// -----------------------------------------------------------------------
// <copyright file="SpellComponents.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("spellComponents")]
    public class SpellComponents
    {
        [XmlAttribute("kinds")]
        [DefaultValue(typeof(SpellComponentKinds), "None")]
        public SpellComponentKinds Kinds { get; set; }

        [XmlText]
        public string Description { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeDescription()
        {
            return !string.IsNullOrWhiteSpace(this.Description);
        }
    }
}