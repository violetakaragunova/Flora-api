using BusinessLayer.Injectors;
using BusinessLayer.Services;
using DataAccessLayer;
using DataTransferLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantTrackerAPI.BusinessLayer.Services;
using PlantTrackerAPI.DataTransferLayer.Interfaces;

namespace CoreLayer.Injectors
{
    public class InjectionRoot
    {
        public static void injectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:ApplicationDbConnection"])
            );
            InjectIdentity.injectIdentity(services,configuration);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
