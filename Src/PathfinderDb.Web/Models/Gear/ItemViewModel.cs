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

        public string Source { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Weight { get; set; }

        public ItemViewModel Load(Gear gear)
        {
            this.Source = gear.Source.ToDisplayString();
            this.Name = gear.Name;
            this.Price = gear.Price.ToDisplayString();
            this.Weight = gear.Weight.ToDisplayString();
            return this;
        }
    }
}