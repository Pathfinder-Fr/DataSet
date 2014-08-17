// -----------------------------------------------------------------------
// <copyright file="GearDocument.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using Schema;

    public class GearDocument : IDocument<Gear>
    {
        private Schema.Gear gear;

        public int DocId { get; set; }

        public string Lang { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public GearCategoryViewModel Category { get; set; }

        public bool HasEnglishName { get; set; }

        public string XmlContent { get; set; }

        private Gear Gear
        {
            get { return this.gear ?? (this.gear = this.Deserialize()); }
            set
            {
                this.gear = value;
                this.Serialize(value);
            }
        }

        public ItemViewModel AsItem()
        {
            return new ItemViewModel
            {
                DocId = this.DocId,
                Name = this.Name,
                Price = this.Gear.Price.ToDisplayString(),
                Weight = this.Gear.Weight.ToDisplayString(),
                Source = this.Gear.Source.ToDisplayString(),
            };
        }

        public EditViewModel AsEdit()
        {
            return new EditViewModel
            {
                // Document
                DocId = this.DocId,
                Category = this.Category,
                Name = this.Name,
                Source = this.Source,
                // Gear
                Price = this.gear.Price.ToEditString(),
                Weight = this.gear.Weight.ToEditString(),
                EnglishName = this.gear.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, "name"),
                Description = this.gear.Description,
            };
        }

        public void Apply(EditViewModel edit)
        {
            var localGear = this.Gear;

            localGear.Category = (GearCategory)(this.Category = edit.Category);
            localGear.Name = this.Name = edit.Name;
            localGear.Source.Id = this.Source = edit.Source;

            this.Gear = localGear;
        }
    }
}