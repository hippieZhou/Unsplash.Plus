using System;
using Unsplash.Plus.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Unsplash.Plus
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel => DataContext as ShellViewModel;
        public Shell()
        {
            this.InitializeComponent();
            ViewModel.Initialize(ContentFrame);

            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromMilliseconds(400);
        }
    }
}
