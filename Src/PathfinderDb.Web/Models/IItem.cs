// -----------------------------------------------------------------------
// <copyright file="IItem.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    public interface IItem<in T, out TViewModel>
    {
        int DocId { get; set; }

        TViewModel Load(T source);
    }
}