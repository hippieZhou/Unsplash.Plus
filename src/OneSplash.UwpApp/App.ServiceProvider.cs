using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.Application;
using OneSplash.Infrastructure;
using OneSplash.UwpApp.ViewModels;
using Serilog;
using System;
using System.IO;
using OneSplash.UwpApp.Extensions;
using Windows.Storage;
using OneSplash.UwpApp.Servcies.Logging;
using Serilog.Events;
using Microsoft.Extensions.Configuration;

namespace OneSplash.UwpApp
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        static App()
        {
            var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.With<CustomDetailsEnricher>()
                .WriteTo.Debug()
                .WriteTo.File(
                  Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs", "log.txt"),
                  rollingInterval: RollingInterval.Day,
                  restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();

            Log.Information("Serilog started!");
        }

        public static IServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddLoggings()
            .AddSettings()
            .AddApplicationLayer()
            .AddPersistenceInfrastructure()
            .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
            .AddSingleton<ShellViewModel>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<SearchViewModel>()
            .AddSingleton<InfoViewModel>()
            .BuildServiceProvider();
    }
}
