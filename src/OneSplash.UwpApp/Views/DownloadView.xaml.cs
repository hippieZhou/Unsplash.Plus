using Microsoft.Toolkit.Mvvm.DependencyInjection;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.ViewModels;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class DownloadView : UserControl
    {
        public DownloadViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<DownloadViewModel>();
        public DownloadView()
        {
            this.InitializeComponent();
        }
    }
}
