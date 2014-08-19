// -----------------------------------------------------------------------
// <copyright file="IDocumentService.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Models.Gear;

    public interface IDocumentService
    {
        IDocumentServiceSession OpenSession();
    }

    public interface IDocumentServiceSession : IDisposable
    {
        IEnumerable<GearDocument> LoadGears(IDocumentQuery<GearDocument> query);

        GearDocument LoadGear(int id);

        void SaveGear(GearDocument doc);
    }

    public interface IDocumentQuery<T>
    {
        IEnumerable<Expression<Func<T, bool>>> AsExpressions();
    }
}