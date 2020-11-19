using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.Application;
using OneSplash.Infrastructure;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.Helpers;
using OneSplash.UwpApp.Servcies.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace OneSplash.UwpApp
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddLoggings()
                .AddSettings()
                .AddViewModels()
                .AddApplicationLayer()
                .AddPersistenceInfrastructure()
                .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                .BuildServiceProvider());

            var configuration = Ioc.Default.GetRequiredService<IConfiguration>();
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

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (Window.Current.Content is null)
            {
                Window.Current.Content = new Shell();
                TitleBarHelper.StyleTitleBar();
                TitleBarHelper.ExpandViewIntoTitleBar();
            }

            if (e.PrelaunchActivated == false)
            {
                CoreApplication.EnablePrelaunch(true);
                Window.Current.Activate();
            }
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
