// -----------------------------------------------------------------------
// <copyright file="FeatEditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.Linq;
    using DataSet;

    public class FeatEditViewModel
    {
        public FeatType[] Types { get; set; }

        public FeatType Type { get; set; }

        public static FeatEditViewModel FromFeat(Feat feat)
        {
            return new FeatEditViewModel
            {
                Types = feat.Types.Select(x => (FeatType)x).ToArray()
            };
        }

        public Feat AsFeat()
        {
            return new Feat
            {
                Types = this.Types.Select(x => (PathfinderDb.DataSet.FeatType)x).ToArray()
            };
        }
    }
}