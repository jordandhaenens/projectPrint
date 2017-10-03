using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectPrintDos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectPrintDos.Data
{
    public static class SeedData
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            
            var roleStore = new RoleStore<IdentityRole>(context);
            var userstore = new UserStore<ApplicationUser>(context);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var role = new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
                await roleStore.CreateAsync(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var role = new IdentityRole { Name = "User", NormalizedName = "USER" };
                await roleStore.CreateAsync(role);
            }

            if (!context.ApplicationUser.Any(u => u.FirstName == "admin"))
            {
                ApplicationUser user = new ApplicationUser {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                var passwordHash = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHash.HashPassword(user, "admin");
                await userstore.CreateAsync(user);
                await userstore.AddToRoleAsync(user, "ADMINISTRATOR");
                await context.SaveChangesAsync();
            }

            if (!context.ApplicationUser.Any(u => u.FirstName == "test"))
            {
                ApplicationUser user = new ApplicationUser {
                    FirstName = "test",
                    LastName = "test",
                    UserName = "test@test.com",
                    NormalizedUserName = "TEST@TEST.COM",
                    Email = "test@test.com",
                    NormalizedEmail = "TEST@TEST.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                var passwordHash = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHash.HashPassword(user, "test");
                await userstore.CreateAsync(user);
                await userstore.AddToRoleAsync(user, "USER");
                await context.SaveChangesAsync();
            }

            if (!context.BillingAddress.Any())
            {
                BillingAddress bAddress = new BillingAddress()
                {
                    Street = "123 First St",   
                    City = "Nashvile",
                    State = "TN",
                    ZipCode = 37102,
                    IsDefault = true,
                    User = await context.ApplicationUser.SingleOrDefaultAsync(u => u.NormalizedUserName == "TEST@TEST.COM")
                };
                BillingAddress otherBAddress = new BillingAddress()
                {
                    Street = "1324 2nd St",   
                    City = "Nashvile",
                    State = "TN",
                    ZipCode = 37201,
                    IsDefault = false,
                    User = await context.ApplicationUser.SingleOrDefaultAsync(u => u.NormalizedUserName == "TEST@TEST.COM")
                };
                context.Add(bAddress);
                context.Add(otherBAddress);
                await context.SaveChangesAsync();
            }

            if (!context.Ink.Any())
            {
                Ink Ink1 = new Ink() 
                {
                    Title = "Cyan",
                    Cost = 2.5,
                    Price = 5,
                    Quantity = 15,
                    Img = "~/images/turquoise.jpeg"
                };

                Ink Ink2 = new Ink() 
                {
                    Title = "GhostWhite",
                    Cost = 2.5,
                    Price = 5,
                    Quantity = 15,
                    Img = "~/images/ghostWhite.jpg"   
                };

                Ink Ink3 = new Ink() 
                {
                    Title = "Black",
                    Cost = 2.5,
                    Price = 5,
                    Quantity = 15,
                    Img = "~/images/black.jpg"   
                };
                
                context.Add(Ink1);
                context.Add(Ink2);
                context.Add(Ink3);
                await context.SaveChangesAsync();
            }

            if (!context.Screen.Any())
            {
                
            }
                
        }
    }
}