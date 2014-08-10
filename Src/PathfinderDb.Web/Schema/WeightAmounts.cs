// -----------------------------------------------------------------------
// <copyright file="WeightAmounts.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class WeightAmounts
    {
        public static string ToDisplayString(this WeightAmount @this)
        {
            if (@this == null)
            {
                return "–";
            }

            if (!string.IsNullOrEmpty(@this.Special))
            {
                return @this.Special;
            }

            if (@this.Value == 0)
            {
                return "–";
            }

            if (@this.Value < 1 && @this.Unit == WeightUnit.Kilogram)
            {
                return string.Format("{0:0} g", @this.Value * 1000);
            }

            return string.Format("{0} {1}", @this.Value, @this.Unit.ToDisplayString());
        }

        public static string ToEditString(this WeightAmount @this)
        {
            if (@this == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(@this.Special))
            {
                return @this.Special;
            }

            if (@this.Value < 1 && @this.Unit == WeightUnit.Kilogram)
            {
                return string.Format("{0:0} g", @this.Value * 1000);
            }

            return string.Format("{0} {1}", @this.Value, @this.Unit.ToDisplayString());
        }

        public static WeightAmount ParseWeight(string weight)
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

            var result = new WeightAmount { Value = value };

            switch (match.Groups["Unit"].Value.ToLowerInvariant())
            {
                    //case "kg":
                default:
                    result.Unit = WeightUnit.Kilogram;
                    break;

                case "g":
                    result.Value /= 1000;
                    result.Unit = WeightUnit.Kilogram;
                    break;

                case "lb":
                    result.Unit = WeightUnit.Pounds;
                    break;
            }

            return result;
        }
    }
}