using Microsoft.Extensions.DependencyInjection;
using OneSplash.Application.Interfaces;
using OneSplash.Shared.Services;

namespace OneSplash.Shared
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            return services;
        }
    }
}
