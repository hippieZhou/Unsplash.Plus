using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Unsplash.Plus.Mappings;
using Unsplash.Plus.Services;
using Unsplash.Plus.Settings;
using Unsplash.Plus.Views;
using Windows.ApplicationModel;

namespace Unsplash.Plus.ViewModels
{
    public class ViewModelLocator
    {
        public IServiceProvider Services { get; }

        public ViewModelLocator()
        {
            Services = ConfigureServices();
        }


        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddSingleton(sp =>
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Package.Current.InstalledLocation.Path)
                .AddJsonFile("appsettings.json", optional: false);
                return builder.Build();
            });

            services.AddSingleton(sp =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GeneralProfile>();
                }).CreateMapper();
            });

            services.AddSingleton<IUnsplashService, UnsplashService>();
            services.AddSingleton<INavigationService, NavigationService>(sp =>
            {
                var nav = new NavigationService();
                nav.Configure(nameof(MainViewModel), typeof(MainView));
                return nav;
            });

            services.AddTransient<ShellViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<DetailViewModel>();
            services.AddTransient<AppSettings>();

            return services.BuildServiceProvider();
        }

        public ShellViewModel Shell => Services.GetRequiredService<ShellViewModel>();
        public MainViewModel Main => Services.GetRequiredService<MainViewModel>();
        public DetailViewModel Detail => Services.GetRequiredService<DetailViewModel>();
        public AppSettings AppSettings => Services.GetRequiredService<AppSettings>();
    }
}
