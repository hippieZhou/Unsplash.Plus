using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.Application.DTOs;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.Servcies.Messages;
using OneSplash.UwpApp.ViewModels;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


namespace OneSplash.UwpApp.Views
{
    public sealed partial class MainView : Page
    {
        public MainViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<MainViewModel>();
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;

            WeakReferenceMessenger.Default.Register<ConnectedNavMessage, string>(SplashGridView, typeof(SplashGridView).FullName, async (sender, args) =>
            {
                if (args.HasReceivedResponse && SplashGridView.AdaptiveGridView.ContainerFromItem(args.Response.SelectedItem) is GridViewItem clickedItem)
                {
                    clickedItem.Opacity = 1.0d;
                    ViewModel.Selected = args.Response.SelectedItem;
                    SplashGridView.AdaptiveGridView.ScrollIntoView(ViewModel.Selected, ScrollIntoViewAlignment.Default);
                    SplashGridView.AdaptiveGridView.UpdateLayout();
                    await SplashGridView.AdaptiveGridView.TryStartConnectedAnimationAsync(args.Response.Animation, ViewModel.Selected, "connectedElement");
                }
            });
        }

        private ICommand _itemClickCommand;
        public ICommand ItemClickCommand
        {
            get
            {
                if (_itemClickCommand == null)
                {
                    _itemClickCommand = new RelayCommand<SplashPhotoDto>(args =>
                    {
                        if (SplashGridView.AdaptiveGridView.ContainerFromItem(args) is GridViewItem clickedItem)
                        {
                            ViewModel.Selected = args;
                            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromSeconds(2);
                            ConnectedAnimation connectedAnimation = SplashGridView.AdaptiveGridView.PrepareConnectedAnimation("forwardAnimation", ViewModel.Selected, "connectedElement");
                            connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
                            connectedAnimation.Completed += (sender, e) => 
                            {
                                clickedItem.Opacity = 0.0d;
                            };
                            var msg = new ConnectedNavMessage();
                            msg.Reply((ViewModel.Selected, connectedAnimation));
                            WeakReferenceMessenger.Default.Send(msg, typeof(OverlayPopup).FullName);
                        }
                    });
                }
                return _itemClickCommand;
            }
        }
    }
}
