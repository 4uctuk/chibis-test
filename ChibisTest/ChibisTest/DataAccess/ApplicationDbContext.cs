using ChibisTest.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChibisTest.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(c => c.Cart)
                .HasForeignKey(c => c.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(c => c.CartItems)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
