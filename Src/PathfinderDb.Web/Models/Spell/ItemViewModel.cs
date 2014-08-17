// -----------------------------------------------------------------------
// <copyright file="ItemViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Spell
{
    using Schema;

    public class ItemViewModel : IItem<Spell, ItemViewModel>
    {
        public int DocId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public ItemViewModel Load(Spell source)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            return this;
        }
    }
}