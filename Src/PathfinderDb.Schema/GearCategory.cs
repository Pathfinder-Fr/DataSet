// -----------------------------------------------------------------------
// <copyright file="GearCategory.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.DataSet
{
    using System.Xml.Serialization;

    [XmlType("gearCategory")]
    public enum GearCategory
    {
        [XmlEnum("adventuring")]
        Adventuring,

        [XmlEnum("toolsAndSkillKits")]
        ToolsAndSkillKits,

        [XmlEnum("animalsMountsAndRelatedGear")]
        AnimalsMountsAndRelatedGear,

        [XmlEnum("clothing")]
        Clothing,

        [XmlEnum("entertainment")]
        Entertainment,

        [XmlEnum("tradeGoods")]
        TradeGoods,

        [XmlEnum("foodDrinkAndLodging")]
        FoodDrinkAndLodging,

        [XmlEnum("services")]
        Services,

        [XmlEnum("transport")]
        Transport,

        [XmlEnum("alchemy")]
        Alchemy,

        [XmlEnum("specialSubstancesAndItems")]
        SpecialSubstancesAndItems,

        [XmlEnum("poisons")]
        Poisons,
    }
}