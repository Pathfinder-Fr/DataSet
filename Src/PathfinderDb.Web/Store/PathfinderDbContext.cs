// -----------------------------------------------------------------------
// <copyright file="PathfinderDbContext.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Datas
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Schema;

    public class PathfinderDbContext : IdentityDbContext<Models.Identity.ApplicationUser>
    {
        public PathfinderDbContext()
            : base("PathfinderDbContext")
        {
        }

        public DbSet<DbDocument> Documents { get; set; }

        public static PathfinderDbContext Create()
        {
            return new PathfinderDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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