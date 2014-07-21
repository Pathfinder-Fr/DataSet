// -----------------------------------------------------------------------
// <copyright file="FeatIndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.Collections.Generic;
    using DataSet;

    public class FeatIndexViewModel
    {
        public IEnumerable<Feat> Feats { get; set; }
    }
}