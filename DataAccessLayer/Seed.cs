using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using PlantTrackerAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataAccessLayer
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
            {
                new Role{Name="User"},
                new Role{Name="Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new User
            {
                UserName = "admin",
                PhoneNumber= "070 000 000"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "User" });
        }
    }
}
