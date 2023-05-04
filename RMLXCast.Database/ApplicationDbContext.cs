using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Orders;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                    .Property(x => x.Price)
                    .HasColumnType("decimal(18,4)");

            builder.Entity<Order>()
                .Property(x => x.TotalPrice)
                .HasColumnType("decimal(18,4)");

            builder.Entity<ProductCategory>()
                .HasData(new ProductCategory
                {
                    Id = 1,
                    Name = "Все",
                    Description = "Все товары",
                    Editable = false
                });

            builder.
                 Entity<Product>()
                .HasMany(x => x.ProductCategories)
                .WithMany(x => x.Products)
                .UsingEntity<ProductProductCategory>(
                    j => j
                      .HasOne(p => p.ProductCategory)
                      .WithMany(s => s.ProductProductCategories)
                      .HasForeignKey(t => t.ProductCategoryId),
                    j => j
                    .HasOne(t => t.Product)
                    .WithMany(p => p.ProductProductCategories)
                    .HasForeignKey(f => f.ProductId),
                    j =>
                    {
                        j.HasKey(t => new { t.ProductId, t.ProductCategoryId });
                        j.ToTable("ProductProductCategories");
                    }
                );
        }
    }
}
