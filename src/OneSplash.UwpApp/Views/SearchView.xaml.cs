using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class SearchView : UserControl
    {
        public SearchViewModel ViewModel { get; } = App.ServiceProvider.GetRequiredService<SearchViewModel>();
        public SearchView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private async void Back_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            await this.StartCollapsedAnimationAsync();
        }
    }
}
