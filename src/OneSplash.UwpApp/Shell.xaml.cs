using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.Servcies.Messages;
using OneSplash.UwpApp.ViewModels;
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
            this.DataContext = ViewModel;
            WeakReferenceMessenger.Default.Register<ConnectedNavMessage, string>(OverlayPopup, typeof(OverlayPopup).FullName, (sender, args) =>
              {
                  if (args.HasReceivedResponse)
                  {
                      args.Response.animation.TryStart(OverlayPopup.MainScrollViewer);
                      OverlayPopup.SelectedItem = args.Response.selectedItem;
                      OverlayPopup.Visibility = Windows.UI.Xaml.Visibility.Visible;
                  }
              });
        }


        private ICommand _connectedNavBackCommand;
        public ICommand ConnectedNavBackCommand
        {
            get
            {
                if (_connectedNavBackCommand == null)
                {
                    _connectedNavBackCommand = new RelayCommand(() =>
                    {
                        ConnectedAnimation connectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", OverlayPopup.MainScrollViewer);
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
                return _connectedNavBackCommand;
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
