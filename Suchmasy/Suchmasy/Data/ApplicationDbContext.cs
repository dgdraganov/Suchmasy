using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Suchmasy.Models;

namespace Suchmasy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasMany(p => p.Suppliers)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            builder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany()
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>(b =>
            {
                b.HasKey(c => c.Id);

                b.HasOne<IdentityUser>()   
                    .WithMany()      
                    .HasForeignKey(c => c.BuyerId)
                    .IsRequired();
            });

            builder.Entity<Request>(b =>
            {
                b.HasKey(c => c.Id);

                b.HasOne<IdentityUser>()    // <---
                    .WithMany()       // <---
                    .HasForeignKey(c => c.RequesterId)
                    .IsRequired();
            });
        }
    }
}