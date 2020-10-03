using System;
using System.Numerics;
using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Unsplash.Plus.Views
{
    public sealed partial class MainView : Page
    {
        private PhotoItem _storeditem;
        public MainViewModel ViewModel => DataContext as MainViewModel;
        public MainView()
        {
            this.InitializeComponent();
        }

        private async void MainGridView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_storeditem != null)
            {
                MainGridView.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.Default);
                MainGridView.UpdateLayout();

                ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackConnectedAnimation");
                if (animation != null)
                {
                    animation.IsScaleAnimationEnabled = true;
                    animation.Configuration = new DirectConnectedAnimationConfiguration();
                    animation.Completed += (_sender, _e) =>
                    {
                        MainGridView.ContainerFromItem(_storeditem).SetValue(OpacityProperty, 1.0d);
                    };
                    await MainGridView.TryStartConnectedAnimationAsync(animation, _storeditem, "PlaceImage");
                }
            }
        }

        private void MainGridView_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (_storeditem != null)
            {
                MainGridView.ScrollIntoView(_storeditem);
                MainGridView.UpdateLayout();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var root = (FrameworkElement)sender;
            var shadowHost = FindVisualChild<Canvas>(root);
            InitializeAnimation(root, shadowHost);
        }

        private void InitializeAnimation(FrameworkElement root, Canvas shadowHost)
        {
            var rootVisual = ElementCompositionPreview.GetElementVisual(root);
            var compositor = rootVisual.Compositor;

            #region 阴影
            var shadowHostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);

            //Create shadow and add it to the Visual Tree
            var shadow = compositor.CreateDropShadow();
            shadow.Color = Color.FromArgb(255, 75, 75, 80);
            var shadowVisual = compositor.CreateSpriteVisual();
            shadowVisual.Shadow = shadow;
            ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", shadowHostVisual);
            shadowVisual.StartAnimation("Size", bindSizeAnimation);
            var shadowAnimation = compositor.CreateExpressionAnimation("100 * (source.Scale.X - 1)");
            shadowAnimation.SetReferenceParameter("source", rootVisual);
            shadow.StartAnimation("BlurRadius", shadowAnimation);
            #endregion

            var pointerEnteredAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerEnteredAnimation.InsertKeyFrame(1.0f, new Vector3(1.02f));

            var pointerExitedAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerExitedAnimation.InsertKeyFrame(1.0f, new Vector3(1.0f));

            root.PointerEntered += (sender, args) =>
            {
                //root.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, root.ActualWidth, root.ActualHeight) };
                rootVisual.CenterPoint = new Vector3(rootVisual.Size / 2, 0);
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

        private void OnMainGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            if (MainGridView.ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                _storeditem = container.Content as PhotoItem;
                var animation = MainGridView.PrepareConnectedAnimation("ForwardConnectedAnimation", _storeditem, "PlaceImage");
                animation.IsScaleAnimationEnabled = true;
                animation.Configuration = new BasicConnectedAnimationConfiguration();
                animation.Completed += (_sender, _e) =>
                {
                    container.Opacity = 0.0d;
                };
            }
            Frame.Navigate(typeof(DetailView), _storeditem, new SuppressNavigationTransitionInfo());
        }
    }
}
