// -----------------------------------------------------------------------
// <copyright file="IEdit.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    public interface IEdit<T, out TViewModel> : IItem<T, TViewModel>
    {
        string Id { get; }

        ViewSubmitAction SubmitAction { get; set; }

        T Save();

        TViewModel AsNew();
    }
}