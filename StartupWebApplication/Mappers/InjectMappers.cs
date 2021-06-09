using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StartupWebApplication.Mappers;
using BusinessUserProfile = BusinessLayer.Mappers.UserProfile;

namespace PlantTrackerAPI.Mappers
{
    public class InjectMappers
    {
        public static IMapper injectMappers(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new BusinessUserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return mapper;
        }
    }
}
