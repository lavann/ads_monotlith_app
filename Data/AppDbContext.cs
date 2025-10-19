
using Microsoft.EntityFrameworkCore;
using RetailMonolith.Models;

namespace RetailMonolith.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<InventoryItem> Inventory => Set<InventoryItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartLine> CartLines => Set<CartLine>();

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();

      


        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
            b.Entity<InventoryItem>().HasIndex(i => i.Sku).IsUnique();
        }

        public static async Task SeedAsync(AppDbContext db)
        {
            if (!await db.Products.AnyAsync())
            {
                var items = new[]
                {
                new Product { Sku="SKU-1001", Name="Classic Tee", Price=19.99m, Category="Apparel", Currency="Stirling" },
                new Product { Sku="SKU-2001", Name="Sneakers", Price=59.99m, Category="Footwear",Currency="Stirling" },
                new Product { Sku="SKU-3001", Name="Backpack", Price=39.99m, Category="Accessories",Currency="Stirling" },
            };
                await db.Products.AddRangeAsync(items);
                await db.Inventory.AddRangeAsync(items.Select(p => new InventoryItem { Sku = p.Sku, Quantity = 100 }));
                await db.SaveChangesAsync();
            }
        }


    }
}
