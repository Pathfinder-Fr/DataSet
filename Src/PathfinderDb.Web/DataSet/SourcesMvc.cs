using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathfinderDb.DataSet
{
    using System.Web.Mvc;

    public class SourcesMvc
    {
        public static readonly IEnumerable<SelectListItem> SelectListAll = new[]
        {
            new SelectListItem { Value = Source.Ids.PathfinderRpg, Text = "Manuel des joueurs"},
            new SelectListItem { Value = Source.Ids.AdvancedPlayerGuide, Text = "Manuel des joueurs - règles avancées"},
            new SelectListItem { Value = Source.Ids.UltimateCombat, Text = "L'Art de la Guerre"},
            new SelectListItem { Value = Source.Ids.UltimateMagic, Text = "L'Art de la Magie"},
            new SelectListItem { Value = Source.Ids.Bestiary, Text = "Bestiaire"},
            new SelectListItem { Value = Source.Ids.Bestiary2, Text = "Bestiaire 2"},
            new SelectListItem { Value = Source.Ids.Bestiary3, Text = "Bestiaire 3"},
            new SelectListItem { Value = Source.Ids.Bestiary4, Text = "Bestiaire 4"},
            new SelectListItem { Value = Source.Ids.Bestiary4, Text = "Bestiaire 4"},
            new SelectListItem { Value = Source.Ids.UltimateMagic, Text = "L'Art de la Magie"},

        };
    }
}