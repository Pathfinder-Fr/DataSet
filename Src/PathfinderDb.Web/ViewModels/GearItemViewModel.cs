// -----------------------------------------------------------------------
// <copyright file="GearIndexItemViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using Schema;

    public class GearItemViewModel : IItem<Gear, GearItemViewModel>
    {
        public int DocId { get; set; }

        public string Name { get; set; }

        public GearItemViewModel Load(Gear gear)
        {
            this.Name = gear.Name;
            return this;
        }
    }
}