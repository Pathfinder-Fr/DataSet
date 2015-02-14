// -----------------------------------------------------------------------
// <copyright file="Trait.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// Character traits are abilities that are not tied to your character's race or class.
    /// They can enhance your character's skills, racial abilities, class abilities, or other statistics, enabling you to further customize him.
    /// </summary>
    /// <remarks>
    /// At its core, a character trait is approximately equal in power to half a feat, so two character traits are roughly equivalent to a bonus feat.
    /// Yet a character trait isn't just another kind of power you can add on to your character—it's a way to quantify (and encourage) building a
    /// character background that fits into your campaign world.
    /// Think of character traits as “story seeds” for your background; after you pick your two traits, you'll have a point of inspiration from which
    /// to build your character's personality and history.
    /// Alternatively, if you've already got a background in your head or written down for your character, you can view picking his traits as a way to
    /// quantify that background, just as picking race and class and ability scores quantifies his other strengths and weaknesses.
    /// </remarks>
    [XmlType("trait", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class Trait : Element
    {
        /// <summary>
        /// Gets or sets the unique id of this trait.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this trait.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public TraitType Type { get; set; }

        [XmlAttribute("category")]
        public string Category { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}