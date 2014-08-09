// -----------------------------------------------------------------------
// <copyright file="FeatType.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Feat
{
    using System.ComponentModel.DataAnnotations;

    public enum FeatTypeViewModel
    {
        [Display(Name = "Combat")]
        Combat = 1,

        [Display(Name = "Critique")]
        Critical = 2,

        [Display(Name = "Création d'objets magiques")]
        ItemCreation = 3,

        [Display(Name = "Méta-magie")]
        Metamagic = 4,

        [Display(Name = "Monstre")]
        Monster = 5,

        [Display(Name = "Équipe")]
        Teamwork = 6,

        [Display(Name = "Audace")]
        Grit = 7,

        [Display(Name = "Spectacle")]
        Performance = 8,

        [Display(Name = "École")]
        Style = 9
    }
}