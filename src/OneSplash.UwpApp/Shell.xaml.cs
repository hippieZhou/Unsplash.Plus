using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel { get; } = App.Locator.Provider.GetRequiredService<ShellViewModel>();
        public Shell()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
