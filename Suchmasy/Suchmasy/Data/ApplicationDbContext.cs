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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany()
                .HasForeignKey(o => o.SupplierId);

            builder.Entity<Order>(b =>
            {
                b.HasKey(c => c.Id);

                b.HasOne<IdentityUser>()    // <---
                    .WithMany()       // <---
                    .HasForeignKey(c => c.BuyerId)
                    .IsRequired();
            });

        }
    }
}