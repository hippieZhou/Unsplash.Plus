using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.Extensions;
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
        private void SearchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SearchView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //SearchView.StartVisibleAnimation();
        }

        private void InfoButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            InfoView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //InfoView.StartVisibleAnimation();
        }
    }
}
