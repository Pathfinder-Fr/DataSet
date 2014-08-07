// -----------------------------------------------------------------------
// <copyright file="PathfinderDbContext.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Schema;
    using ViewModels;

    public class PathfinderDbContext : DbContext
    {
        public DbSet<DbDocument> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DbDocument>()
                .HasKey(x => x.DocId);
        }
    }

    public static class DbSetExtensions
    {
        public static List<TViewModel> AsSchema<TViewModel>(this DbSet<DbDocument> @this, Func<DbDocument, TViewModel> transform)
        {

            return @this
                .Where(d => d.Type == DbDocumentType.Gear && d.Lang == DataSetLanguages.French)
                .ToList()
                .Select(transform)
                .ToList();
        }

    }
}