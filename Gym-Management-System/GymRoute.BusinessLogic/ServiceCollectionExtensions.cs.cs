using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Interceptors; 
using GymRoute.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymRoute.BusinessLogic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {

            services.AddScoped<IMemberRepository, MemberRepository>(); 

            return services;
        }
    }
}