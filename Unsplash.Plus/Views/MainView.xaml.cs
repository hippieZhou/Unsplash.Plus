using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Numerics;
using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
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
        public MainViewModel ViewModel => DataContext as MainViewModel;
        public MainView()
        {
            this.InitializeComponent();
        }

        private void MainGridView_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (sender is AdaptiveGridView gridView && gridView.SelectedItem != null)
            {
                gridView.ScrollIntoView(gridView.SelectedItem);
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
            ViewModel.SelectedItem = e.ClickedItem as PhotoItem;
            MainGridView.PrepareConnectedAnimation("mainToDetail", e.ClickedItem, "PlaceImage");
            Frame.Navigate(typeof(DetailView), ViewModel.SelectedItem, new SuppressNavigationTransitionInfo());
        }
    }
}
