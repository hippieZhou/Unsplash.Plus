using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.UwpApp.Extensions;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class SplashGridViewHeader : UserControl
    {
        private CompositionPropertySet _scrollerPropertySet;
        private Compositor _compositor;
        private CompositionPropertySet _props;

        public SplashGridViewHeader()
        {
            this.InitializeComponent();
        }

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(SplashGridViewHeader), new PropertyMetadata(DependencyProperty.UnsetValue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SplashGridViewHeader), new PropertyMetadata(DependencyProperty.UnsetValue));

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var gridView = this.FindAscendant<GridView>();
            if (gridView == null)
                return;

            var scrollViewer = gridView.FindDescendant<ScrollViewer>();
            var headerPresenter = (UIElement)VisualTreeHelper.GetParent((UIElement)gridView.Header);
            var headerContainer = (UIElement)VisualTreeHelper.GetParent(headerPresenter);
            Canvas.SetZIndex(headerContainer, 1);

            _scrollerPropertySet = scrollViewer.GetScrollViewerManipulationPropertySet();
            _compositor = _scrollerPropertySet.Compositor;

            _props = _compositor.CreatePropertySet();
            _props.InsertScalar("progress", 0);
            _props.InsertScalar("clampSize", 240);
            _props.InsertScalar("scaleFactor", 0.7f);

            var scrollingProperties = _scrollerPropertySet.GetSpecializedReference<ManipulationPropertySetReferenceNode>();
            var props = _props.GetReference();
            var progressNode = props.GetScalarProperty("progress");
            var clampSizeNode = props.GetScalarProperty("clampSize");
            var scaleFactorNode = props.GetScalarProperty("scaleFactor");

            ExpressionNode progressAnimation = ExpressionFunctions.Clamp(-scrollingProperties.Translation.Y / clampSizeNode, 0, 1);
            _props.StartAnimation("progress", progressAnimation);

            ExpressionNode headerScaleAnimation = ExpressionFunctions.Lerp(1, 1.25f, ExpressionFunctions.Clamp(scrollingProperties.Translation.Y / 50, 0, 1));
            ExpressionNode headerTranslationAnimation = ExpressionFunctions.Conditional(progressNode < 1, 0, -scrollingProperties.Translation.Y - clampSizeNode);

            var headerVisual = this.ElementVisual();
            headerVisual.CenterPoint = new Vector3((float)(this.ActualWidth / 2), (float)this.ActualHeight, 0);
            headerVisual.StartAnimation("Scale.X", headerScaleAnimation);
            headerVisual.StartAnimation("Scale.Y", headerScaleAnimation);
            headerVisual.StartAnimation("Offset.Y", headerTranslationAnimation);

            ExpressionNode primaryOpacityAnimation = 1 - progressNode;
            PrimaryBackground.ElementVisual().StartAnimation("opacity", primaryOpacityAnimation);

            ExpressionNode secondaryOpacityAnimation = progressNode;
            SecondaryBackground.ElementVisual().StartAnimation("opacity", secondaryOpacityAnimation);

            ExpressionNode scaleAnimation = ExpressionFunctions.Lerp(1, scaleFactorNode, progressNode);
            ExpressionNode opacityAnimation = ExpressionFunctions.Clamp(1 - (progressNode * 2), 0, 1);
            Visual autoSearchBoxVisual = SearchBox.ElementVisual();
            autoSearchBoxVisual.CenterPoint = new Vector3((float)(SearchBox.ActualWidth / 2), (float)SearchBox.ActualHeight, 0);
            autoSearchBoxVisual.StartAnimation("Scale.X", scaleAnimation);
            autoSearchBoxVisual.StartAnimation("Scale.Y", scaleAnimation);
            autoSearchBoxVisual.StartAnimation("Opacity", opacityAnimation);

            Visual subMenuPanelVisual = SubMenuPanel.ElementVisual();
            ExpressionNode contentOffsetAnimation = progressNode * 100;
            subMenuPanelVisual.StartAnimation("Offset.Y", contentOffsetAnimation);

            ExpressionNode searchBtnOpacityAnimation = ExpressionFunctions.Conditional(progressNode < 1, 0, 1);
            Visual searchBtnVisual = ElementCompositionPreview.GetElementVisual(SearchBtn);
            searchBtnVisual.StartAnimation("Opacity", searchBtnOpacityAnimation);
        }
    }
}
