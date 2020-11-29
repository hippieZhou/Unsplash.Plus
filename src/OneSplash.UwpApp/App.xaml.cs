using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.Application;
using OneSplash.Infrastructure;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.Helpers;
using OneSplash.UwpApp.Servcies.Logging;
using OneSplash.UwpApp.Servcies.Navigation;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        public App()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.With<CustomDetailsEnricher>()
                .WriteTo.Debug()
                .WriteTo.File(
                Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs", "log.txt"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
            Log.Information("Serilog started!");

            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (Window.Current.Content is null)
            {
                Ioc.Default.ConfigureServices(ConfigureServices());

                Window.Current.Content = new Shell();
                TitleBarHelper.ExpandViewIntoTitleBar();
                TitleBarHelper.UpdateTitleBar();

                ThemeHelper.Initialize();
            }

            if (e.PrelaunchActivated == false)
            {
                CoreApplication.EnablePrelaunch(true);
                Window.Current.Activate();
            }
        }

        public void EnableSound(bool withSpatial = false)
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
            ElementSoundPlayer.SpatialAudioMode = !withSpatial ? ElementSpatialAudioMode.Off : ElementSpatialAudioMode.On;
        }

        private IServiceProvider ConfigureServices() => new ServiceCollection()
            .AddLoggings()
            .AddSettings()
            .AddViewModels()
            .AddApplicationLayer()
            .AddPersistenceInfrastructure()
            .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
            .AddSingleton<INavigationService, NavigationService>()
            .BuildServiceProvider();
    }
}
