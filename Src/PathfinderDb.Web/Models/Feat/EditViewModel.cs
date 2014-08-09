// -----------------------------------------------------------------------
// <copyright file="FeatEditViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Feat
{
    using System.Linq;
    using Schema;

    public class EditViewModel
    {
        public FeatTypeViewModel[] Types { get; set; }

        public FeatTypeViewModel Type { get; set; }

        public static EditViewModel FromFeat(Feat feat)
        {
            return new EditViewModel
            {
                Types = feat.Types.Select(x => (FeatTypeViewModel)x).ToArray()
            };
        }

        public Feat AsFeat()
        {
            return new Feat
            {
                Types = this.Types.Select(x => (PathfinderDb.Schema.FeatType)x).ToArray()
            };
        }
    }
}