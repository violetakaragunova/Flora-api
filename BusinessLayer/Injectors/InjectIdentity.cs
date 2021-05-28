using DataAccessLayer;
using DomainModel.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlantTrackerAPI.DomainModel;

namespace BusinessLayer.Injectors
{
    public class InjectIdentity
    {
        public static void injectIdentity(IServiceCollection services)
        {
            services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
