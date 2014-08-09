// -----------------------------------------------------------------------
// <copyright file="FeatIndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Feat
{
    using System.Collections.Generic;
    using Schema;

    public class IndexViewModel
    {
        public IEnumerable<Feat> Feats { get; set; }
    }
}