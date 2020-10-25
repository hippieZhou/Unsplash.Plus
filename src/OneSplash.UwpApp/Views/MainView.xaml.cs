using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class MainView : UserControl
    {
        public MainViewModel ViewModel { get; } = App.Startup.Provider.GetRequiredService<MainViewModel>();
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
