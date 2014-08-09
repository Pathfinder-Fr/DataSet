// -----------------------------------------------------------------------
// <copyright file="GearCategory.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using System.ComponentModel.DataAnnotations;

    public enum GearCategoryViewModel
    {
        [Display(Name = "Matériel d'aventurier")]
        Adventuring,

        [Display(Name = "Matériel de classes et de compétences")]
        ToolsAndSkillKits,

        [Display(Name = "Animaux, montures et équipement")]
        AnimalsMountsAndRelatedGear,

        [Display(Name = "Vêtements")]
        Clothing,

        [Display(Name = "Jeux")]
        Entertainment,

        [Display(Name = "Marchandises")]
        TradeGoods,

        [Display(Name = "Nourriture, boissons et hébergement")]
        FoodDrinkAndLodging,

        [Display(Name = "Services")]
        Services,

        [Display(Name = "Transport")]
        Transport,

        [Display(Name = "Alchimie")]
        Alchemy,

        [Display(Name = "Substances et objets spéciaux")]
        SpecialSubstancesAndItems,

        [Display(Name = "Poisons")]
        Poisons,
    }
}