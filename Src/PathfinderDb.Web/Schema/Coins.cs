// -----------------------------------------------------------------------
// <copyright file="CoinExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Coins
    {
        public static string ToDisplayString(this Coin @this)
        {
            switch (@this)
            {
                case Coin.Copper:
                    return Properties.Resources.CoinCopper;

                default:
                case Coin.Gold:
                    return Properties.Resources.CoinGold;

                case Coin.Platinium:
                    return Properties.Resources.CoinPlatinium;

                case Coin.Silver:
                    return Properties.Resources.CoinSilver;
            }
        }
    }
}