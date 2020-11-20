using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.ViewModels.Widgets;
using Windows.UI.Xaml.Controls;


namespace OneSplash.UwpApp.Views.Widgets
{
    public sealed partial class MoreWidgetView : UserControl
    {
       public MoreWidgetViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<MoreWidgetViewModel>();

        public MoreWidgetView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            this.Loaded += (sender, e) => ViewModel.IsActive = true;
            this.Unloaded += (sender, e) => ViewModel.IsActive = false;
        }

        private void Back_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
