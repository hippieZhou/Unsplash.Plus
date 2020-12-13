using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.Application.DTOs;
using OneSplash.UwpApp.Controls;
using OneSplash.UwpApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

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

                            ViewModel.SelectedItem = args;
                            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromSeconds(1.0);
                            ConnectedAnimation connectedAnimation = SplashGridView.AdaptiveGridView.PrepareConnectedAnimation("forwardAnimation", ViewModel.SelectedItem, "connectedElement");
                            connectedAnimation.IsScaleAnimationEnabled = true;
                            connectedAnimation.Configuration = new BasicConnectedAnimationConfiguration();
                            connectedAnimation.Completed += (sender, e) =>
                            {
                                clickedItem.Opacity = 0.0d;
                            };
                            connectedAnimation.TryStart(OverlayPopup.destinationElement);

                            OverlayPopup.SelectedItem = ViewModel.SelectedItem;
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
                                ViewModel.SelectedItem = OverlayPopup.SelectedItem;
                                SplashGridView.AdaptiveGridView.ScrollIntoView(ViewModel.SelectedItem, ScrollIntoViewAlignment.Default);
                                await SplashGridView.AdaptiveGridView.TryStartConnectedAnimationAsync(connectedAnimation, ViewModel.SelectedItem, "connectedElement");
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

        private ICommand _downloadCommand;
        public ICommand DownloadCommand
        {
            get
            {
                if (_downloadCommand == null)
                {
                    _downloadCommand = new RelayCommand(() =>
                    {
                        var download = Ioc.Default.GetRequiredService<DownloadViewModel>();
                        if (download != null)
                        {
                            download.IsPaneShow = true;
                        }
                    });
                }
                return _downloadCommand;
            }
        }

        private ICommand _moreCommand;
        public ICommand MoreCommand
        {
            get
            {
                if (_moreCommand == null)
                {
                    _moreCommand = new RelayCommand(() =>
                    {
                        var shell = Ioc.Default.GetRequiredService<ShellViewModel>();
                        if (shell != null)
                        {
                            shell.IsPaneOpen = true;
                        }
                    });
                }
                return _moreCommand;
            }
        }

        private bool _firstTimeAnimation = true;
        private string _dotAnimationKey = "storeSplash";
        private async void OnItemDownload(object sender, RoutedEventArgs e)
        {
            if (sender is Button handler &&
                handler.FindParentByName("connectedElement") is Grid root &&
                root.FindChildByName("HeroImage") is ImageEx heroImage &&
                root.FindChildByName("HeroImageMirror") is Image heroImageMirror &&
                SplashGridView.Header is SplashGridViewHeader header)
            {
                handler.IsEnabled = false;

                var bitmap = new RenderTargetBitmap();
                await bitmap.RenderAsync(heroImage);
                heroImageMirror.Source = bitmap;

                ViewModel.DownloadCommand?.Execute(root.DataContext);

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(_dotAnimationKey, heroImageMirror);
                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(_dotAnimationKey);
                var dot = header.FindDescendant<Ellipse>();
                animation?.TryStart(dot);

                // JL: Need to figutre out why the first time the animation doesn't run although animation returns true.
                if (_firstTimeAnimation)
                {
                    _firstTimeAnimation = false;
                    await Task.Delay(50);
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate(_dotAnimationKey, heroImageMirror);
                    var animation1 = ConnectedAnimationService.GetForCurrentView().GetAnimation(_dotAnimationKey);
                    animation1?.TryStart(dot);
                }

                dot.Visibility = Visibility.Visible;
                handler.IsEnabled = true;
            }
        }
    }
}
