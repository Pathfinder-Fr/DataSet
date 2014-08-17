// -----------------------------------------------------------------------
// <copyright file="IEdit.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    public interface IEdit<T, out TViewModel> : IItem<T, TViewModel>
    {
        string Id { get; }

        ViewSubmitAction SubmitAction { get; set; }

        /// <summary>
        /// Save state from this edition model to the provided schema if it already exists, or create a new schema item, then returns it.
        /// </summary>
        /// <param name="existing">An existing schema in case of an update, or <c>null</c> if it is a new schema.</param>
        /// <returns>The existing schema if provided, or the new one if <paramref name="existing"/> was <c>null</c>.</returns>
        T Save(T existing);

        TViewModel AsNew();
    }
}