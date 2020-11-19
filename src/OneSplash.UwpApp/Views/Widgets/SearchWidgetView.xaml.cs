using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.ViewModels.Widgets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views.Widgets
{
    public sealed partial class SearchWidgetView : UserControl
    {
        public SearchWidgetViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<SearchWidgetViewModel>();
        public SearchWidgetView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            this.Loaded += (sender, e) => ViewModel.IsActive = true;
            this.Unloaded += (sender, e) => ViewModel.IsActive = false;
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            await this.StartCollapsedAnimationAsync();
        }
    }
}
