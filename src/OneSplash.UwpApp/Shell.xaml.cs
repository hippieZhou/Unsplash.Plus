using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.UI.Animations;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.Helpers;
using OneSplash.UwpApp.Servcies.Messages;
using OneSplash.UwpApp.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace OneSplash.UwpApp
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<ShellViewModel>();
        public Shell()
        {
            this.InitializeComponent();
            ViewModel.Initialize(ContentFrame, MainNav);
            DataContext = ViewModel;
            WeakReferenceMessenger.Default.Register<ConnectedNavMessage, string>(OverlayPopup, typeof(OverlayPopup).FullName, (sender, args) =>
             {
                 if (args.HasReceivedResponse)
                 {
                     args.Response.Animation.TryStart(OverlayPopup.destinationElement);
                     OverlayPopup.SelectedItem = args.Response.SelectedItem;
                     OverlayPopup.Visibility = Windows.UI.Xaml.Visibility.Visible;
                 }
             });
        }

        private void OnOverlayPopupHideClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromSeconds(2);
            ConnectedAnimation connectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", OverlayPopup.destinationElement);
            connectedAnimation.Completed += (_sender, _e) =>
           {
                OverlayPopup.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
           };
            connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
            var msg = new ConnectedNavMessage();
            msg.Reply((OverlayPopup.SelectedItem, connectedAnimation));
            WeakReferenceMessenger.Default.Send(msg, typeof(SplashGridView).FullName);
        }
    }
}
