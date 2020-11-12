using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OneSplash.UwpApp.Extensions;
using OneSplash.UwpApp.ViewModels;
using System.Numerics;
using Windows.UI;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;
using System;
using OneSplash.Application.DTOs;
using OneSplash.UwpApp.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class MainView : UserControl
    {
        public MainViewModel ViewModel { get; } = App.ServiceProvider.GetRequiredService<MainViewModel>();
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void CategoryGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var root = (FrameworkElement)sender;
            InitializeAnimation(root, root.FindVisualChild<Canvas>());
        }

        private void SplashGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var root = (FrameworkElement)sender;
            InitializeAnimation(root.FindVisualChild<ImageEx>(), null, true);
        }

        private void InitializeAnimation(FrameworkElement root, FrameworkElement shadowHost,bool cliped = false)
        {
            var rootVisual = ElementCompositionPreview.GetElementVisual(root);
            var compositor = rootVisual.Compositor;

            if (shadowHost != null)
            {
                var shadowHostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);

                // Create shadow and add it to the Visual Tree
                var shadow = compositor.CreateDropShadow();
                shadow.Color = Color.FromArgb(255, 75, 75, 80);
                shadow.BlurRadius = 25f;
                var shadowVisual = compositor.CreateSpriteVisual();
                shadowVisual.Shadow = shadow;
                ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

                // Make sure the shadow resizes as its host resizes
                var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
                bindSizeAnimation.SetReferenceParameter("hostVisual", shadowHostVisual);
                shadowVisual.StartAnimation("Size", bindSizeAnimation);

                // Increase the blurradius as the rectangle is scaled up
                var shadowAnimation = compositor.CreateExpressionAnimation("100 * (source.Scale.X - 1)");
                shadowAnimation.SetReferenceParameter("source", rootVisual);
                shadow.StartAnimation("BlurRadius", shadowAnimation);
            }

            // Create animation to scale up the rectangle
            var pointerEnteredAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerEnteredAnimation.InsertKeyFrame(1.0f, new Vector3(1.1f));

            // Create animation to scale the rectangle back down
            var pointerExitedAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerExitedAnimation.InsertKeyFrame(1.0f, new Vector3(1.0f));

            // Play animations on pointer enter and exit
            root.PointerEntered += (sender, args) =>
            {
                if (cliped)
                {
                    var parent = root.FindAscendant<FrameworkElement>();
                    parent.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, root.ActualWidth, root.ActualHeight) };
                }

                rootVisual.CenterPoint = new Vector3(rootVisual.Size / 2, 0);
                rootVisual.StartAnimation("Scale", pointerEnteredAnimation);
            };

            root.PointerExited += (sender, args) => rootVisual.StartAnimation("Scale", pointerExitedAnimation);
        }

        private void SplashGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SplashGridView.ContainerFromItem(e.ClickedItem) is GridViewItem ClickedItem)
            {
                var selectedItem = ClickedItem.Content as SplashPhotoDto;
                OverlayPopup.SelectedItem = selectedItem;
                ConnectedAnimation ConnectedAnimation = SplashGridView.PrepareConnectedAnimation("forwardAnimation", selectedItem, "connectedElement");
                ConnectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
                ConnectedAnimation.TryStart(OverlayPopup.MainScrollViewer);
                OverlayPopup.Visibility = Visibility.Visible;
            }
        }

        private async void OverlayPopup_HandleBack(object sender, object e)
        {
            var selectedItem = OverlayPopup.SelectedItem;
            if (selectedItem != null)
            {
                SplashGridView.ScrollIntoView(selectedItem, ScrollIntoViewAlignment.Default);
                SplashGridView.UpdateLayout();

                ConnectedAnimation ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", OverlayPopup.MainScrollViewer);
                ConnectedAnimation.Completed += (_sender, _e) =>
                {
                    OverlayPopup.Visibility = Visibility.Collapsed;
                };
                ConnectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
                await SplashGridView.TryStartConnectedAnimationAsync(ConnectedAnimation, selectedItem, "connectedElement");
            }
        }
    }
}
