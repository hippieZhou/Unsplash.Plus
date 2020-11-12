using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.Application;
using OneSplash.Infrastructure;
using OneSplash.UwpApp.ViewModels;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using OneSplash.UwpApp.Extensions;
using Windows.Storage;

namespace OneSplash.UwpApp
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        static App()
        {
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.Debug()
              .WriteTo.File(
                Path.Combine(ApplicationData.Current.LocalFolder.Path, "logs/log.txt"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Warning)
              .CreateLogger();

            Log.Information("Serilog started!");
        }

        public static IServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddLogging()
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
