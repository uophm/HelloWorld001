using DbProjectExample.Models;
using Microsoft.EntityFrameworkCore;

namespace DbProjectExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Milk", Price = 3.50m, Category = "Dairy" },
                new Product { Id = 2, Name = "Bread", Price = 2.00m, Category = "Bakery" },
                new Product { Id = 3, Name = "Eggs", Price = 4.20m, Category = "Dairy" },
                new Product { Id = 4, Name = "Apples", Price = 5.00m, Category = "Produce" },
                new Product { Id = 5, Name = "Bananas", Price = 1.50m, Category = "Produce" },
                new Product { Id = 6, Name = "Chicken Breast", Price = 10.00m, Category = "Meat" },
                new Product { Id = 7, Name = "Rice", Price = 8.00m, Category = "Grains" },
                new Product { Id = 8, Name = "Pasta", Price = 1.20m, Category = "Grains" },
                new Product { Id = 9, Name = "Cereal", Price = 4.50m, Category = "Breakfast" },
                new Product { Id = 10, Name = "Coffee", Price = 12.00m, Category = "Beverages" }
            );
        }
    }
}
