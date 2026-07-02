using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Interceptors; 
using GymRoute.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymRoute.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<AuditColumnsInterceptor>();

            services.AddDbContext<GymDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);

                var auditInterceptor = sp.GetRequiredService<AuditColumnsInterceptor>();
                options.AddInterceptors(auditInterceptor);
            });

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(Repository<>));
            services.AddScoped<IMemberRepository, MemberRepository>();

            return services;
        }
    }
}