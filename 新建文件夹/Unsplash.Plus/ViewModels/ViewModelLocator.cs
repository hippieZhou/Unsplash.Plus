using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace Unsplash.Plus.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(services =>
            {
                services.AddSingleton<ShellViewModel>();
            });
        }

        public ShellViewModel Shell => Ioc.Default.GetRequiredService<ShellViewModel>();
    }
}
