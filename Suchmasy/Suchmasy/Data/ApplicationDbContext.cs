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

        DbSet<Order> Orders { get; set; }
        DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Order>()
                .HasOne(o => o.Supplier);

            builder.Entity<Order>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.BuyerId).IsRequired();

                // Without referencing navigation properties (they're not there anyway)
                b.HasOne<IdentityUser>()    // <---
                    .WithMany()       // <---
                    .HasForeignKey(c => c.BuyerId);
            });

        }
    }
}