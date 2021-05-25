using DataAccessLayer;
using DomainModel.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Injectors
{
    public class InjectIdentity
    {
        public static void injectIdentity(IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
