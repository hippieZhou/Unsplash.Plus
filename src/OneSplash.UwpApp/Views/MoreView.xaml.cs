using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class MoreView : UserControl
    {
        public MoreViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<MoreViewModel>();
        public MoreView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
