// -----------------------------------------------------------------------
// <copyright file="PathfinderDbContext.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System.Data.Entity;

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
}