using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class InfoView : UserControl
    {
        public InfoViewModel ViewModel { get; } = App.Locator.Provider.GetRequiredService<InfoViewModel>();
        public InfoView()
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
