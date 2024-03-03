using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace DiplomOnlineShop
{
    public class OnlineShopContext:DbContext
    {
        public OnlineShopContext(DbContextOptions<OnlineShopContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders).UsingEntity(
                    "OrderProduct",
                    l => l.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey("Id"),
                    r => r.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId").HasPrincipalKey("Id"),
                    j => j.HasKey("ProductId", "OrderId"));
        }
    }
}
