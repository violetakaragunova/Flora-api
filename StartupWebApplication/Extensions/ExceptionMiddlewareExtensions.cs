using Microsoft.AspNetCore.Builder;
using PlantTrackerAPI.CustomExceptionMiddleware;

namespace PlantTrackerAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
