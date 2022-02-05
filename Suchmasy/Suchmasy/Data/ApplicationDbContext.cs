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
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasMany(p => p.Suppliers)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Order>(b =>
            {
                b.HasKey(c => c.Id);

                b.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(c => c.BuyerId)
                    .IsRequired();

                b.HasOne(o => o.Supplier)
                    .WithMany()
                    .HasForeignKey(o => o.SupplierId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(o => o.Product)
                    .WithMany()
                    .HasForeignKey(o => o.ProductId);

                b.HasOne(o => o.Request)
                    .WithMany()
                    .HasForeignKey(o => o.RequestId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Delivery>(d =>
            {
                d.HasOne(d => d.Order)
                    .WithOne()
                    .HasForeignKey<Delivery>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);

                d.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(d => d.DriverId);
            });

            builder.Entity<Delivery>(b =>
            {
                b.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(d => d.DriverId);
            });

            builder.Entity<Request>(b =>
            {
                b.HasKey(c => c.Id);

                b.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(c => c.RequesterId)
                    .IsRequired();

                b.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(c => c.ClosedById)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}