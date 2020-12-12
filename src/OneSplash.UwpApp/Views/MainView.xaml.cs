using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.Application.DTOs;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.ViewModels;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;


namespace OneSplash.UwpApp.Views
{
    public sealed partial class MainView : UserControl
    {
        public MainViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<MainViewModel>();
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
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
                            FindName("OverlayPopup");

                            ViewModel.Selected = args;
                            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromSeconds(1.0);
                            ConnectedAnimation connectedAnimation = SplashGridView.AdaptiveGridView.PrepareConnectedAnimation("forwardAnimation", ViewModel.Selected, "connectedElement");
                            connectedAnimation.IsScaleAnimationEnabled = true;
                            connectedAnimation.Configuration = new BasicConnectedAnimationConfiguration();
                            connectedAnimation.Completed += (sender, e) =>
                            {
                                clickedItem.Opacity = 0.0d;
                            };
                            connectedAnimation.TryStart(OverlayPopup.destinationElement);

                            OverlayPopup.SelectedItem = ViewModel.Selected;
                            OverlayPopup.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        }
                    });
                }
                return _itemClickCommand;
            }
        }

        private ICommand _tappedCommand;
        public ICommand TappedCommand
        {
            get
            {
                if (_tappedCommand == null)
                {
                    _tappedCommand = new RelayCommand<TappedRoutedEventArgs>(async args =>
                    {
                        if (args.OriginalSource.Equals(OverlayPopup.rootGrid))
                        {
                            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromSeconds(2);
                            ConnectedAnimation connectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", OverlayPopup.destinationElement);
                            connectedAnimation.Completed += (_sender, _e) =>
                            {
                                OverlayPopup.SelectedItem = null;
                            };
                            connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();

                            if (SplashGridView.AdaptiveGridView.ContainerFromItem(OverlayPopup.SelectedItem) is GridViewItem clickedItem)
                            {
                                clickedItem.Opacity = 1.0d;
                                ViewModel.Selected = OverlayPopup.SelectedItem;
                                SplashGridView.AdaptiveGridView.ScrollIntoView(ViewModel.Selected, ScrollIntoViewAlignment.Default);
                                await SplashGridView.AdaptiveGridView.TryStartConnectedAnimationAsync(connectedAnimation, ViewModel.Selected, "connectedElement");
                                OverlayPopup.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            }
                            args.Handled = true;
                        }
                    });
                }
                return _tappedCommand;
            }
        }

        private ICommand _backToTopCommand;
        public ICommand BackToTopCommand
        {
            get
            {
                if (_backToTopCommand == null)
                {
                    _backToTopCommand = new RelayCommand(() =>
                    {
                        var scrollViewer = SplashGridView.AdaptiveGridView.FindDescendant<ScrollViewer>();
                        if (scrollViewer != null)
                        {
                            scrollViewer.ChangeView(null, 0, null, false);
                        }
                    });
                }
                return _backToTopCommand;
            }
        }
    }
}
