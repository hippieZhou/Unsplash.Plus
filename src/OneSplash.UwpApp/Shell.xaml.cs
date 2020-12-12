using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<ShellViewModel>();
        public Shell()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
