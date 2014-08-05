// -----------------------------------------------------------------------
// <copyright file="GearIndexItemViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using Schema;

    public class GearIndexItemViewModel
    {
        public int DocId { get; set; }

        public string Name { get; set; }

        public static GearIndexItemViewModel FromDataSet(Models.DbDocument doc, Gear gear)
        {
            return new GearIndexItemViewModel { Name = gear.Name, DocId = doc.DocId };
        }
    }
}