using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneSplash.Application;
using OneSplash.Infrastructure;
using OneSplash.UwpApp.ViewModels;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.Storage;

namespace OneSplash.UwpApp
{
    public class Locator
    {
        public IServiceProvider Provider { get; private set; }

        private readonly IConfiguration _config;
        public Locator()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Package.Current.InstalledLocation.Path)
                .AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(_config)
              .Enrich.FromLogContext()
              .WriteTo.Debug()
              .WriteTo.File(
                Path.Combine(ApplicationData.Current.LocalFolder.Path, "logs/log.txt"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Warning)
              .CreateLogger();

            Log.Information("Serilog started!");

            Provider = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            #region Services
            services.AddLogging();
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructure(_config);
            #endregion

            #region ViewModels
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SearchViewModel>();
            services.AddSingleton<InfoViewModel>();
            #endregion

            return services.BuildServiceProvider();
        }
    }
}
