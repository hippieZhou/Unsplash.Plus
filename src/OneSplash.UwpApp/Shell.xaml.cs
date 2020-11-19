using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.Extensions;
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
            this.DataContext = ViewModel;
        }
        private async void SearchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SearchView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await SearchView.StartVisibleAnimationAsync();
        }

        private async void InfoButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            InfoView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await InfoView.StartVisibleAnimationAsync();
        }
    }
}
