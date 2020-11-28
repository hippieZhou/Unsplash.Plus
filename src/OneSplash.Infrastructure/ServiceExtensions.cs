using Microsoft.Extensions.DependencyInjection;
using OneSplash.Domain.Interfaces;
using OneSplash.Infrastructure.Contexts;
using OneSplash.Infrastructure.Repositories;
using OneSplash.Infrastructure.Shared.DateTime;
using OneSplash.Infrastructure.Shared.Unsplash;

namespace OneSplash.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationDbContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IUnSplashPhotoService, UnSplashDataService>();
            //services.AddTransient<UnSplashDataService>();
            //services.AddTransient<Func<bool, ISplashService>>(serviceProvider => key =>
            //{
            //    return key ? serviceProvider.GetService<UnSplashDataService>() : serviceProvider.GetService<UnSplashDataService>();
            //});
            return services;
        }
    }
}
