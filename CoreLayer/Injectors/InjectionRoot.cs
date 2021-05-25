using BusinessLayer.Injectors;
using BusinessLayer.Services;
using DataAccessLayer;
using DataTransferLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLayer.Injectors
{
    public class InjectionRoot
    {
        public static void injectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:ApplicationDbConnection"])
            );
            InjectIdentity.injectIdentity(services);
            services.AddScoped<IUserService, UserService>();
        }
    }
}
