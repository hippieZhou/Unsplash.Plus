using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Unsplash.Plus.Mappings;
using Unsplash.Plus.Services;
using Unsplash.Plus.Settings;
using Unsplash.Plus.Views;
using Windows.ApplicationModel;

namespace Unsplash.Plus.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(service =>
            {
                service.AddLogging();

                service.AddSingleton<AppSettings>();

                service.AddSingleton(sp =>
                {
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(Package.Current.InstalledLocation.Path)
                    .AddJsonFile("appsettings.json", optional: false);
                    return builder.Build();
                });

                service.AddSingleton(sp =>
                {
                    return new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<GeneralProfile>();
                    }).CreateMapper();
                });

                service.AddSingleton<ShellViewModel>();
                service.AddSingleton<MainViewModel>();
                service.AddSingleton<IUnsplashService, UnsplashService>();
                service.AddSingleton<INavigationService, NavigationService>(sp =>
                {
                    var nav = new NavigationService();
                    nav.Configure(nameof(MainViewModel), typeof(MainView));
                    return nav;
                });
            });
        }

        public ShellViewModel Shell => Ioc.Default.GetRequiredService<ShellViewModel>();
        public MainViewModel Main => Ioc.Default.GetRequiredService<MainViewModel>();
        public AppSettings AppSettings => Ioc.Default.GetRequiredService<AppSettings>();
    }
}
