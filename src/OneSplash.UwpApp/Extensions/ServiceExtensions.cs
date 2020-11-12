using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneSplash.Domain.Settings;
using Windows.ApplicationModel;

namespace OneSplash.UwpApp.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Package.Current.InstalledLocation.Path)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            return services;
        }
    }
}
