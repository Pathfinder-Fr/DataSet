// -----------------------------------------------------------------------
// <copyright file="GearIndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.Collections.Generic;

    public class Page<T>
    {
        public List<T> Items { get; set; }
    }
}