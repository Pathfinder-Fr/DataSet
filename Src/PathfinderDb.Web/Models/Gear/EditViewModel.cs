// -----------------------------------------------------------------------
// <copyright file="GearEditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Properties;
    using Schema;

    public class EditViewModel : IEdit<Gear, EditViewModel>
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

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Price_Name", Prompt = "GearEditViewModel_Price_Prompt", Description = "GearEditViewModel_Price_Description")]
        [CustomValidation(typeof(EditViewModel), "ValidatePrice")]
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

        public EditViewModel Load(Gear gear)
        {
            this.Category = (GearCategoryViewModel)gear.Category;
            this.Source = gear.Source.Id;
            this.Price = gear.Price.ToEditString();
            this.Weight = gear.Weight.ToEditString();
            this.Name = gear.Name;
            this.EnglishName = gear.OpenLocalization().GetLocalizedEntry(DataSetLanguages.English, "name");
            this.Description = gear.Description;

            return this;
        }

        public Gear Save(Gear existing)
        {
            var gear = existing ?? new Gear();

            gear.Name = this.Name;
            gear.Price = MoneyAmounts.ParsePrice(this.Price);
            gear.Weight = WeightAmounts.ParseWeight(this.Weight);
            gear.Category = (GearCategory)this.Category;
            gear.Id = this.Id;
            gear.Description = this.Description;
            gear.Source.Id = this.Source;

            if (!string.IsNullOrEmpty(this.EnglishName))
            {
                gear.OpenLocalization().AddLocalizedEntry(DataSetLanguages.English, "name", this.EnglishName);
            }

            return gear;
        }

        public EditViewModel AsNew()
        {
            return new EditViewModel
            {
                Category = this.Category,
                Source = this.Source
            };
        }

        //public void Saved(Datas.DbDocument dbDoc)
        //{
        //    this.DocId = dbDoc.DocId;
        //}
    }
}