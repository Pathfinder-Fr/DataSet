// -----------------------------------------------------------------------
// <copyright file="DbDocumentType.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum DbDocumentType
    {
        Unknown = 0,

        [Display(Name = "Dons")]
        Feat,

        [Display(Name = "Équipements")]
        Gear,

        [Display(Name = "Sorts")]
        Spells,

        [Display(Name = "Monstres")]
        Monsters,

        [Display(Name = "Traits")]
        Traits,

        [Display(Name = "Armes")]
        Weapons,

        [Display(Name = "Armures")]
        Armors
    }
}