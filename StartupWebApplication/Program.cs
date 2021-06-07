using DataAccessLayer;
using DomainModel.Identity;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantTrackerAPI.BusinessLayer;
using PlantTrackerAPI.DataAccessLayer;
using PlantTrackerAPI.DomainModel;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StartupWebApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            var logger = host.Services.GetRequiredService<ILoggerManager>();


            try
            {
                var context = services.GetRequiredService<ApplicationContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(userManager, roleManager);
                throw new Exception(message:"Error in program.cs");
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error in program.cs");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
             {
                webBuilder.UseStartup<Startup>();
             });
    }
}
