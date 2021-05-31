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
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };

               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context => {
                       var accessToken = context.Request.Query["access_token"];

                       var path = context.HttpContext.Request.Path;

                       if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                       {
                           context.Token = accessToken;
                       }

                       return Task.CompletedTask;
                   }
               };
           });


            return services;
        }
    }
}
