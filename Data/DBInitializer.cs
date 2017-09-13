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
                var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                await roleStore.CreateAsync(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var role = new IdentityRole { Name = "User", NormalizedName = "User" };
                await roleStore.CreateAsync(role);
            }

            if (!context.ApplicationUser.Any(u => u.FirstName == "admin"))
            {
                //  This method will be called after migrating to the latest version.
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
                user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
                await userstore.CreateAsync(user);
                await userstore.AddToRoleAsync(user, "Administrator");
                }
        }
    }
}