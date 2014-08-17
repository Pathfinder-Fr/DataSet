// -----------------------------------------------------------------------
// <copyright file="EditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Spell
{
    using System.Web.Mvc;
    using Schema;

    public class EditViewModel : IEdit<Spell, EditViewModel>
    {
        [HiddenInput(DisplayValue = false)]
        public int DocId { get; set; }

        public string Id
        {
            get { return Ids.Normalize(this.Name); }
        }

        public string Name { get; set; }

        public ViewSubmitAction SubmitAction { get; set; }

        public Spell Save(Spell existing)
        {
            var spell = existing ?? new Spell();
            spell.Id = this.Id;
            spell.Name = this.Name;

            return spell;
        }

        public EditViewModel AsNew()
        {
            return new EditViewModel();
        }

        public EditViewModel Load(Spell source)
        {
            this.Name = source.Name;

            return this;
        }
    }
}