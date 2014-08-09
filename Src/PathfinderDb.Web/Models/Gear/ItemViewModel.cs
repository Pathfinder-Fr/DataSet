// -----------------------------------------------------------------------
// <copyright file="GearIndexItemViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using Schema;

    public class ItemViewModel : IItem<Gear, ItemViewModel>
    {
        public int DocId { get; set; }

        public string Name { get; set; }

        public ItemViewModel Load(Gear gear)
        {
            this.Name = gear.Name;
            return this;
        }
    }
}