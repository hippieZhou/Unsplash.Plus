using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneSplash.Domain.Settings;
using Serilog;
using System.IO;
using Windows.ApplicationModel;
using Windows.Storage;

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
            services.Configure<AppSettings>(appSettings =>
            {
                var conf = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
                appSettings.AccessKey = conf.AccessKey;
                appSettings.Secret = conf.Secret;
                appSettings.DBFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite");
            });
            return services;
        }
        public static IServiceCollection AddLoggings(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            return services;
        }
    }
}
