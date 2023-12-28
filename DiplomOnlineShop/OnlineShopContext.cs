using Microsoft.EntityFrameworkCore;
using System;

namespace DiplomOnlineShop
{
    public class OnlineShopContext:DbContext
    {
        public OnlineShopContext(DbContextOptions<OnlineShopContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
