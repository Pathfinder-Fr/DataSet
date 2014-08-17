// -----------------------------------------------------------------------
// <copyright file="ISchemaAdapter.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System;
    using Datas;
    using Schema;

    public interface ISchemaAdapter
    {
        DbDocumentType DocType { get; }

        Type SchemaType { get; }

        string GetName(Element schema);
        string GetCategory(Element schema);

        string GetNameProperty();
    }
}