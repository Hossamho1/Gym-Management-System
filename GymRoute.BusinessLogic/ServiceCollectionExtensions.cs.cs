using GymRoute.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymRoute.BusinessLogic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IMembrService, MembrService>();

            return services;
        }
    }
}