// -----------------------------------------------------------------------
// <copyright file="GearEditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Properties;

    public class GearEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DocId { get; set; }

        public readonly string Lang = Schema.DataSetLanguages.French;

        public string Id
        {
            get { return Schema.Ids.Normalize(this.Name); }
        }

        [UIHint("Source")]
        public string Source { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "GearEditViewModel_Category_Name")]
        public GearCategory Category { get; set; }

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
            return (ParsePrice(price, out message) != null && !string.IsNullOrEmpty(price)) ? ValidationResult.Success : new ValidationResult(message);
        }

        public static GearEditViewModel FromSchema(Models.DbDocument doc, Schema.Gear gear)
        {
            var vm = new GearEditViewModel();
            vm.DocId = doc.DocId;
            vm.Category = (GearCategory)gear.Category;
            vm.Source = gear.Source.Id;
            vm.Price = Schema.MoneyAmountExtensions.ToDisplayString(gear.Price);
            vm.Weight = Schema.WeightAmountExtensions.ToDisplayString(gear.Weight);
            vm.Name = gear.Name;
            vm.EnglishName = gear.OpenLocalization().GetLocalizedEntry(Schema.DataSetLanguages.English, "name");
            vm.Description = gear.Description;

            return vm;
        }

        public Schema.Gear AsSchema(Schema.Gear gear = null)
        {
            gear = gear ?? new Schema.Gear();

            gear.Name = this.Name;
            gear.Price = ParsePrice(this.Price);
            gear.Weight = ParseWeight(this.Weight);
            gear.Category = (Schema.GearCategory)this.Category;
            gear.Id = this.Id;
            gear.Source.Id = this.Source;
            gear.Description = this.Description;

            if (!string.IsNullOrEmpty(this.EnglishName))
            {
                gear.OpenLocalization().AddLocalizedEntry(Schema.DataSetLanguages.English, "name", this.EnglishName);
            }

            return gear;
        }

        public static Schema.WeightAmount ParseWeight(string weight)
        {
            if (string.IsNullOrEmpty(weight))
            {
                return null;
            }

            var match = Regex.Match(weight, @"(?<Value>\d+(\.\d{1,2})?)\s*(?<Unit>kg|g|lb|)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            if (!match.Success)
            {
                return null;
            }

            int value;
            if (!int.TryParse(match.Groups["Value"].Value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value))
            {
                return null;
            }

            var result = new Schema.WeightAmount { Value = value };

            switch (match.Groups["Unit"].Value.ToLowerInvariant())
            {
                //case "kg":
                default:
                    result.Unit = Schema.WeightUnit.Kilogram;
                    break;

                case "g":
                    result.Value /= 1000;
                    result.Unit = Schema.WeightUnit.Kilogram;
                    break;

                case "lb":
                    result.Unit = Schema.WeightUnit.Pounds;
                    break;
            }

            return result;
        }

        public static Schema.MoneyAmount ParsePrice(string price)
        {
            string msg;
            return ParsePrice(price, out msg);
        }

        public static Schema.MoneyAmount ParsePrice(string price, out string message)
        {
            if (string.IsNullOrEmpty(price))
            {
                message = "Il faut renseigner un prix";
                return null;
            }

            var match = Regex.Match(price, @"(?<Value>\d+(\.\d{1,2})?)\s*(?<Unit>pp|po|pa|pc|gp|sp|cp|)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            if (!match.Success)
            {
                message = "Le prix doit être indiqué sous la forme \"valeur unité\". Les unités reconnues sont : po, pp, pa, pc, gp, sp, cp. L'unité par défaut est le po.";
                return null;
            }

            int value;
            if (!int.TryParse(match.Groups["Value"].Value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value))
            {
                message = "Le prix doit être un nombre valide";
                return null;
            }

            var result = new Schema.MoneyAmount { Value = value };

            switch (match.Groups["Unit"].Value.ToLowerInvariant())
            {
                default:
                    result.Coin = Schema.Coin.Gold;
                    break;

                case "pp":
                    result.Coin = Schema.Coin.Platinium;
                    break;

                case "pa":
                case "sp":
                    result.Coin = Schema.Coin.Silver;
                    break;

                case "pc":
                case "cp":
                    result.Coin = Schema.Coin.Copper;
                    break;
            }

            message = null;
            return result;
        }
    }
}