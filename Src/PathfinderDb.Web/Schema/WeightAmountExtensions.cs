// -----------------------------------------------------------------------
// <copyright file="WeightAmountExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class WeightAmountExtensions
    {
        public static string ToDisplayString(this WeightAmount @this)
        {
            if (@this == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(@this.Special))
            {
                return @this.Special;
            }

            return string.Format("{0} {1}", @this.Value, @this.Unit.ToDisplayString());
        }
    }
}