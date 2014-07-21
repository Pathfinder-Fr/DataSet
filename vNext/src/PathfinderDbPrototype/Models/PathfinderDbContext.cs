using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace PathfinderDb.Models
{
    /// <summary>
    /// Summary description for PathfinderDbContext
    /// </summary>
    public class PathfinderDbContext : DbContext
    {
        public PathfinderDbContext(IServiceProvider serviceProvider, IOptionsAccessor<PathfinderDbContextOptions> optionsAccessor)
                    : base(serviceProvider, optionsAccessor.Options)
        {

        }

        public DbSet<DbDocument> Documents { get; set; }
        //public DbSet<Artist> Artists { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        //public DbSet<CartItem> CartItems { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DbDocument>().Key(a => a.Id);
            //builder.Entity<Artist>().Key(a => a.ArtistId);
            //builder.Entity<Order>().Key(o => o.OrderId).Properties(p => p.Property(o => o.OrderId).ColumnName("Order"));
            //builder.Entity<Genre>().Key(g => g.GenreId);
            //builder.Entity<CartItem>().Key(c => c.CartItemId);
            //builder.Entity<OrderDetail>().Key(o => o.OrderDetailId);
        }
    }

    public class PathfinderDbContextOptions : DbContextOptions
    {

    }
}