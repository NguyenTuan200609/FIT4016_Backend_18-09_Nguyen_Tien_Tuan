// Data/OrderDbContext.cs
using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Models;

namespace OrderManagementApp.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=OrderManagementDB;
                      Trusted_Connection=True;MultipleActiveResultSets=true;
                      TrustServerCertificate=True");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình sử dụng Fluent API
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
                entity.HasIndex(p => p.Sku).IsUnique();
            });
            
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(o => o.OrderNumber).IsUnique();
                entity.HasIndex(o => o.CustomerEmail).IsUnique();
                
                entity.HasOne(o => o.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(o => o.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}