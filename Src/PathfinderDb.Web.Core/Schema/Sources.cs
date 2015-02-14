// -----------------------------------------------------------------------
// <copyright file="Sources.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class Sources
    {
        public static readonly IDictionary<string, string> All = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { Source.Ids.PathfinderRpg, "Manuel des joueurs" },
            { Source.Ids.AdvancedPlayerGuide, "Manuel des joueurs - règles avancées" },
            { Source.Ids.UltimateCombat, "L'Art de la Guerre" },
            { Source.Ids.UltimateMagic, "L'Art de la Magie" },
            { Source.Ids.GameMasteryGuide, "Guide du maître" },
            { Source.Ids.UltimateEquipment, "Armes & équipement" },
            { Source.Ids.AdvancedRaceGuide, "Manuel des races" },
            { Source.Ids.UltimateCampaign, "Guide de campagne" },
            { Source.Ids.MythicAdventures, "Campagnes mythiques" },
            { Source.Ids.Bestiary, "Bestiaire" },
            { Source.Ids.Bestiary2, "Bestiaire 2" },
            { Source.Ids.Bestiary3, "Bestiaire 3" },
            { Source.Ids.Bestiary4, "Bestiaire 4" }
        };

        public static readonly IEnumerable<SelectListItem> SelectListAll = new[]
        {
            new SelectListItem { Value = Source.Ids.PathfinderRpg, Text = "Manuel des joueurs" },
            new SelectListItem { Value = Source.Ids.AdvancedPlayerGuide, Text = "Manuel des joueurs - règles avancées" },
            new SelectListItem { Value = Source.Ids.UltimateCombat, Text = "L'Art de la Guerre" },
            new SelectListItem { Value = Source.Ids.UltimateMagic, Text = "L'Art de la Magie" },
            new SelectListItem { Value = Source.Ids.GameMasteryGuide, Text = "Guide du maître" },
            new SelectListItem { Value = Source.Ids.UltimateEquipment, Text = "Armes & équipement" },
            new SelectListItem { Value = Source.Ids.AdvancedRaceGuide, Text = "Manuel des races" },
            new SelectListItem { Value = Source.Ids.UltimateCampaign, Text = "Guide de campagne" },
            new SelectListItem { Value = Source.Ids.MythicAdventures, Text = "Campagnes mythiques" },
            new SelectListItem { Value = Source.Ids.Bestiary, Text = "Bestiaire" },
            new SelectListItem { Value = Source.Ids.Bestiary2, Text = "Bestiaire 2" },
            new SelectListItem { Value = Source.Ids.Bestiary3, Text = "Bestiaire 3" },
            new SelectListItem { Value = Source.Ids.Bestiary4, Text = "Bestiaire 4" }
        };

        public static string ToDisplayString(this Source @this)
        {
            if (@this == null)
            {
                return string.Empty;
            }

            return IdToDisplayString(@this.Id);
        }

        public static string IdToDisplayString(string id)
        {
            if (id == null)
            {
                return string.Empty;
            }

            var index = id.IndexOf('#');

            if (index != -1)
            {
                id = id.Substring(0, index);
            }

            string text;
            if (All.TryGetValue(id, out text))
            {
                return text;
            }

            return id;
        }
    }
}