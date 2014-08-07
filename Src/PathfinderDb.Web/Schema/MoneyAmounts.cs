// -----------------------------------------------------------------------
// <copyright file="MoneyAmountExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class MoneyAmounts
    {
        public static MoneyAmount ParsePrice(string price)
        {
            string msg;
            return ParsePrice(price, out msg);
        }

        public static MoneyAmount ParsePrice(string price, out string message)
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

            var result = new MoneyAmount { Value = value };

            switch (match.Groups["Unit"].Value.ToLowerInvariant())
            {
                default:
                    result.Coin = Coin.Gold;
                    break;

                case "pp":
                    result.Coin = Coin.Platinium;
                    break;

                case "pa":
                case "sp":
                    result.Coin = Coin.Silver;
                    break;

                case "pc":
                case "cp":
                    result.Coin = Coin.Copper;
                    break;
            }

            message = null;
            return result;
        }

        public static string ToDisplayString(this MoneyAmount @this)
        {
            if (!string.IsNullOrEmpty(@this.Special))
            {
                return @this.Special;
            }

            return string.Format("{0} {1}", @this.Value, @this.Coin.ToDisplayString());
        }
    }
}