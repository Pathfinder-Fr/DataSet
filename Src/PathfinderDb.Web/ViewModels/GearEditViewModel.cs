// -----------------------------------------------------------------------
// <copyright file="GearEditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Properties;
    using Schema;

    public class GearEditViewModel : IEdit<Gear, GearEditViewModel>
    {
        [HiddenInput(DisplayValue = false)]
        public int DocId { get; set; }

        public string Id
        {
            get { return Ids.Normalize(this.Name); }
        }

        [UIHint("Source")]
        public string Source { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Category_Name")]
        public GearCategoryViewModel Category { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Name_Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_EnglishName_Name")]
        public string EnglishName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_SubCategory_Name")]
        public string SubCategory { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Price_Name", Prompt = "GearEditViewModel_Price_Prompt")]
        [CustomValidation(typeof(GearEditViewModel), "ValidatePrice")]
        public string Price { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Weight_Name", Prompt = "GearEditViewModel_Weight_Prompt")]
        public string Weight { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ViewSubmitAction SubmitAction { get; set; }

        public static ValidationResult ValidatePrice(string price)
        {
            string message;
            return (MoneyAmounts.ParsePrice(price, out message) != null && !string.IsNullOrEmpty(price)) ? ValidationResult.Success : new ValidationResult(message);
        }

        public GearEditViewModel Load(Gear gear)
        {
            this.Category = (GearCategoryViewModel)gear.Category;
            this.Source = gear.Source.Id;
            this.Price = gear.Price.ToDisplayString();
            this.Weight = gear.Weight.ToDisplayString();
            this.Name = gear.Name;
            this.EnglishName = gear.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, "name");
            this.Description = gear.Description;

            return this;
        }

        public Gear Save()
        {
            var gear = new Gear
            {
                Name = this.Name,
                Price = MoneyAmounts.ParsePrice(this.Price),
                Weight = WeightAmounts.ParseWeight(this.Weight),
                Category = (GearCategory)this.Category,
                Id = this.Id,
                Description = this.Description,
                Source =
                {
                    Id = this.Source
                },
            };

            if (!string.IsNullOrEmpty(this.EnglishName))
            {
                gear.OpenLocalization().AddLocalizedEntry(DataSetLanguages.English, "name", this.EnglishName);
            }

            return gear;
        }

        public GearEditViewModel AsNew()
        {
            return new GearEditViewModel
            {
                Category = this.Category,
                Source = this.Source
            };
        }

        public void Saved(Models.DbDocument dbDoc)
        {
            this.DocId = dbDoc.DocId;
        }
    }
}