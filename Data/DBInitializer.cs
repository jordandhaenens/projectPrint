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
                await userstore.AddToRoleAsync(user, "ADMINISTRATOR");
                await context.SaveChangesAsync();
            }

            if (!context.ApplicationUser.Any(u => u.FirstName == "test"))
            {
                //  This method will be called after migrating to the latest version.
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
        }
    }
}