using System;
using System.Numerics;
using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Unsplash.Plus.Views
{
    public sealed partial class MainView : Page
    {
        private PhotoItem _selectedPlace;
        public MainViewModel ViewModel => DataContext as MainViewModel;
        public MainView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            var animation = MainGridView.PrepareConnectedAnimation("mainToDetail", _selectedPlace, "PlaceImage");
            animation.IsScaleAnimationEnabled = true;
            animation.Configuration = new BasicConnectedAnimationConfiguration();
            animation.Completed += (_s, _e) =>
            {
                if (MainGridView.ContainerFromItem(_selectedPlace) is GridViewItem item)
                {
                    item.Opacity = 0.0d;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("backwardToMain");
            if (animation != null)
            {
                if (MainGridView.ContainerFromItem(_selectedPlace) is GridViewItem item)
                {
                    animation.Completed += (_s, _e) =>
                    {
                        item.Opacity = 1.0d;
                    };
                    animation.TryStart(item);

                    MainGridView.ScrollIntoView(item);
                    MainGridView.UpdateLayout();
                }
            }
        }

        private void MainGridView_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (_selectedPlace != null)
            {
                MainGridView.ScrollIntoView(_selectedPlace);
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
            _selectedPlace = e.ClickedItem as PhotoItem;
            Frame.Navigate(typeof(DetailView), _selectedPlace, new SuppressNavigationTransitionInfo());
        }
    }
}
