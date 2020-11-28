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
                if (args.HasReceivedResponse)
                {
                    ViewModel.Selected = args.Response.selectedItem;
                    SplashGridView.AdaptiveGridView.ScrollIntoView(ViewModel.Selected, ScrollIntoViewAlignment.Default);
                    SplashGridView.AdaptiveGridView.UpdateLayout();
                    await SplashGridView.AdaptiveGridView.TryStartConnectedAnimationAsync(args.Response.animation, ViewModel.Selected, "connectedElement");
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
                            var msg = new ConnectedNavMessage();
                            msg.Reply((ViewModel.Selected, connectedAnimation));
                            WeakReferenceMessenger.Default.Send(msg, typeof(OverlayPopup).FullName);
                        }
                    });
                }
                return _itemClickCommand;
            }
        }


        //private void CategoryGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var root = (FrameworkElement)sender;
        //    InitializeAnimation(root, root.FindVisualChild<Canvas>());
        //}

        //private void SplashGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var root = (FrameworkElement)sender;
        //    root.FindName("connectedElement");
        //    InitializeAnimation(root.FindVisualChild<ImageEx>(), null, true);
        //}

        //private void InitializeAnimation(FrameworkElement root, FrameworkElement shadowHost, bool cliped = false)
        //{
        //    var rootVisual = ElementCompositionPreview.GetElementVisual(root);
        //    var compositor = rootVisual.Compositor;

        //    if (shadowHost != null)
        //    {
        //        var shadowHostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);

        //        // Create shadow and add it to the Visual Tree
        //        var shadow = compositor.CreateDropShadow();
        //        shadow.Color = Color.FromArgb(255, 75, 75, 80);
        //        shadow.BlurRadius = 25f;
        //        var shadowVisual = compositor.CreateSpriteVisual();
        //        shadowVisual.Shadow = shadow;
        //        ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

        //        // Make sure the shadow resizes as its host resizes
        //        var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
        //        bindSizeAnimation.SetReferenceParameter("hostVisual", shadowHostVisual);
        //        shadowVisual.StartAnimation("Size", bindSizeAnimation);

        //        // Increase the blurradius as the rectangle is scaled up
        //        var shadowAnimation = compositor.CreateExpressionAnimation("100 * (source.Scale.X - 1)");
        //        shadowAnimation.SetReferenceParameter("source", rootVisual);
        //        shadow.StartAnimation("BlurRadius", shadowAnimation);
        //    }

        //    // Create animation to scale up the rectangle
        //    var pointerEnteredAnimation = compositor.CreateVector3KeyFrameAnimation();
        //    pointerEnteredAnimation.InsertKeyFrame(1.0f, new Vector3(1.1f));

        //    // Create animation to scale the rectangle back down
        //    var pointerExitedAnimation = compositor.CreateVector3KeyFrameAnimation();
        //    pointerExitedAnimation.InsertKeyFrame(1.0f, new Vector3(1.0f));

        //    // Play animations on pointer enter and exit
        //    root.PointerEntered += (sender, args) =>
        //    {
        //        if (cliped)
        //        {
        //            var parent = root.FindAscendant<FrameworkElement>();
        //            parent.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, root.ActualWidth, root.ActualHeight) };
        //        }

        //        rootVisual.CenterPoint = new Vector3(rootVisual.Size / 2, 0);
        //        rootVisual.StartAnimation("Scale", pointerEnteredAnimation);
        //    };

        //    root.PointerExited += (sender, args) => rootVisual.StartAnimation("Scale", pointerExitedAnimation);
        //}
    }
}
