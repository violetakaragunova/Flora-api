using DataAccessLayer;
using DomainModel.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using PlantTrackerAPI.DomainModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Injectors
{
    public class InjectIdentity
    {
        public static IServiceCollection injectIdentity(IServiceCollection services , IConfiguration config)
        {
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:TokenKey"])),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });


            return services;
        }
    }
}
