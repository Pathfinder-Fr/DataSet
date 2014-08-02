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

    public class GearEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string OriginalId { get; set; }

        [UIHint("Source")]
        public string Source { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Nom anglais")]
        public string EnglishName { get; set; }

        [Display(Name = "Catégorie")]
        public GearCategory Category { get; set; }

        [Display(Name = "Sous-catégorie")]
        public string SubCategory { get; set; }

        [Display(Name = "Prix", Prompt = "valeur et unité (po par défaut) : 4, 5po, 15pa, etc.")]
        [CustomValidation(typeof(GearEditViewModel), "ValidatePrice")]
        public string Price { get; set; }

        [Display(Name = "Poids", Prompt = "1.5kg, 500g, etc.")]
        public string Weight { get; set; }

        public static bool ValidatePrice(string price)
        {
            return ParsePrice(price) != null;
        }

        public DataSet.Gear AsDataSet()
        {
            return null;
        }

        public static DataSet.MoneyAmount ParsePrice(string price)
        {
            var match = Regex.Match(price, @"(?<Value>\d+(\.\d{1,2}))\s*(?<Unit>pp|po|pa|pc|gp|sp|cp|)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            if (!match.Success)
            {
                return null;
            }

            int value;
            if (!int.TryParse(match.Groups["Value"].Value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value))
            {
                return null;
            }

            var result = new DataSet.MoneyAmount { Value = value };

            switch (match.Groups["Unit"].Value.ToLowerInvariant())
            {
                default:
                    result.Coin = DataSet.Coin.Gold;
                    break;

                case "pp":
                    result.Coin = DataSet.Coin.Platinium;
                    break;

                case "pa":
                case "sp":
                    result.Coin = DataSet.Coin.Silver;
                    break;

                case "pc":
                case "cp":
                    result.Coin = DataSet.Coin.Copper;
                    break;
            }

            return result;
        }
    }
}