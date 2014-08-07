// -----------------------------------------------------------------------
// <copyright file="WeightUnitExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class WeightUnits
    {
        public static string ToDisplayString(this WeightUnit @this)
        {
            switch (@this)
            {
                case WeightUnit.Kilogram:
                    return "kg";

                default:
                    return "lb";
            }
        }
    }
}