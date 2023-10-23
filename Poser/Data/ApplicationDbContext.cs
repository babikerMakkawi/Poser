using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poser.Models;
using Poser.Models.Orders;
using Poser.Models.Products;
using static NuGet.Packaging.PackagingConstants;

namespace Poser.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /// </Customers>
        public DbSet<Customer> Customers { get; set; }

        /// <Brands - Categories>
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        /// <Attributes>
        public DbSet<Models.Products.Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }

        /// <Products>
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        /// </Orders>
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<ProductStock>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductStocks)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Product)
                .WithMany()
                .HasForeignKey(pa => pa.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.ProductStock)
                .WithMany()
                .HasForeignKey(pa => pa.ProductStockId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Attribute)
                .WithMany()
                .HasForeignKey(pa => pa.AttributeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.AttributeValue)
                .WithMany()
                .HasForeignKey(pa => pa.AttributeValueId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}