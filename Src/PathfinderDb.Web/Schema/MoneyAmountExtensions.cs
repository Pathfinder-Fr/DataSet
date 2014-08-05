// -----------------------------------------------------------------------
// <copyright file="MoneyAmountExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class MoneyAmountExtensions
    {
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