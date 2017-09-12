using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projectPrint.Models;

namespace projectPrint.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<projectPrint.Models.Screen> Screen { get; set; }

        public DbSet<projectPrint.Models.ShippingAddress> ShippingAddress { get; set; }

        public DbSet<projectPrint.Models.ProductType> ProductType { get; set; }

        public DbSet<projectPrint.Models.PaymentType> PaymentType { get; set; }

        public DbSet<projectPrint.Models.Order> Order { get; set; }

        public DbSet<projectPrint.Models.Ink> Ink { get; set; }

        public DbSet<projectPrint.Models.CompositeProduct> CompositeProduct { get; set; }

        public DbSet<projectPrint.Models.BillingAddress> BillingAddress { get; set; }

    }
}
