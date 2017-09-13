using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectPrintDos.Models;

namespace ProjectPrintDos.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Screen> Screen { get; set; }

        public DbSet<ShippingAddress> ShippingAddress { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Ink> Ink { get; set; }

        public DbSet<CompositeProduct> CompositeProduct { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<BillingAddress> BillingAddress { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
