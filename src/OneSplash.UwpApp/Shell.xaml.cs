using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<ShellViewModel>();
        public Shell()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(2012, 1004);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(502, 1006));
            DataContext = ViewModel;
        }
    }
}
