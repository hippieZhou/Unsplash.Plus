using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OneSplash.UwpApp.ViewModels;
using System;
using System.Diagnostics;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class MainView : UserControl
    {
        public MainViewModel ViewModel { get; } = App.Locator.Provider.GetRequiredService<MainViewModel>();
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void CategoryGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var root = (FrameworkElement)sender;
            InitializeAnimation(root, FindVisualChild<Canvas>(root));
        }

        private void RecipeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var root = (FrameworkElement)sender;
            InitializeAnimation(FindVisualChild<ImageEx>(root), null, true);
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
                    var parent = VisualTreeHelper.GetParent(root) as FrameworkElement;
                    //root.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, root.ActualWidth, root.ActualHeight) };
                    rootVisual.CenterPoint = new Vector3(
                        (float)(parent.ActualSize.X / 2.0),
                        (float)(parent.ActualSize.Y / 2.0), 0f);
                }
                else
                {
                    rootVisual.CenterPoint = new Vector3(rootVisual.Size / 2, 0);
                }
             
                rootVisual.StartAnimation("Scale", pointerEnteredAnimation);
            };

            root.PointerExited += (sender, args) => rootVisual.StartAnimation("Scale", pointerExitedAnimation);
        }

        private TChild FindVisualChild<TChild>(DependencyObject obj) where TChild : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TChild found)
                    return found;
                else
                {
                    TChild childOfChild = FindVisualChild<TChild>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
