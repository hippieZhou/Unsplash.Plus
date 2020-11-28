using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.Servcies.Messages;
using OneSplash.UwpApp.ViewModels;
using System;
using System.Windows.Input;
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
            ViewModel.Initialize(MainNav, ContentFrame);
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

        private ICommand _hideOverlayPopupCommand;
        public ICommand HideOverlayPopupCommad
        {
            get
            {
                if (_hideOverlayPopupCommand == null)
                {
                    _hideOverlayPopupCommand = new RelayCommand(() =>
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
                    });
                }
                return _hideOverlayPopupCommand;
            }
        }


        //private async void SearchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    SearchView.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    await SearchView.StartVisibleAnimationAsync();
        //}

        //private async void InfoButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    InfoView.Visibility = Windows.UI.Xaml.Visibility.Visible;
        //    await InfoView.StartVisibleAnimationAsync();
        //}
    }
}
