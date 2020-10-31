using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.UwpApp.Extensions;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Controls
{
    /// <summary>
    /// https://blog.pieeatingninjas.be/2016/01/17/custom-uwp-control-step-through-listview/
    /// https://www.cnblogs.com/hupo376787/p/11837285.html
    /// https://www.arthurrump.com/2018/07/21/animating-gridviewitems-with-windows-ui-composition-aka-the-visual-layer/
    /// </summary>
    public sealed partial class StepThroughListView : UserControl, INotifyPropertyChanged
    {
        private ScrollViewer _InternalListScrollViewer;
        private long horizontalOffsetPropertyCallbackToken;
        private long scrollableWidthPropertyCallbackToken;

        public StepThroughListView()
        {
            this.InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                if (SelectedItem != value)
                {
                    SetValue(SelectedItemProperty, value);
                    RaisePropertyChanged();
                }
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set
            {
                SetValue(ItemTemplateProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));


        private void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            void UpdateButtonState()
            {
                var leftBtnStateName = _InternalListScrollViewer.HorizontalOffset == 0
             ? nameof(LeftButtonDisabled) : nameof(LeftButtonEnabled);
                VisualStateManager.GoToState(this, leftBtnStateName, false);

                var rightBtnStateName = _InternalListScrollViewer.HorizontalOffset + _InternalListScrollViewer.ActualWidth < _InternalListScrollViewer.ExtentWidth
                    ? nameof(RightButtonDisabled) : nameof(RightButtonEnabled);
                VisualStateManager.GoToState(this, rightBtnStateName, false);
            }

            _InternalListScrollViewer = ((DependencyObject)sender).FindVisualChild<ScrollViewer>();
            horizontalOffsetPropertyCallbackToken = _InternalListScrollViewer.RegisterPropertyChangedCallback(ScrollViewer.HorizontalOffsetProperty,
                (_sender, _e) => UpdateButtonState());
            scrollableWidthPropertyCallbackToken = _InternalListScrollViewer.RegisterPropertyChangedCallback(ScrollViewer.ScrollableWidthProperty,
                (_sender, _e) => UpdateButtonState());
        }

        private void ListView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_InternalListScrollViewer != null)
            {
                _InternalListScrollViewer.UnregisterPropertyChangedCallback(ScrollViewer.HorizontalOffsetProperty, horizontalOffsetPropertyCallbackToken);
                _InternalListScrollViewer.UnregisterPropertyChangedCallback(ScrollViewer.ScrollableWidthProperty, scrollableWidthPropertyCallbackToken);
            }
        }

        private void ArrowLeft_Click(object sender, RoutedEventArgs e) => ScrollListHorizontal(false);
        private void ArrowRight_Click(object sender, RoutedEventArgs e) => ScrollListHorizontal(true);
        private void ScrollListHorizontal(bool shouldScrollHorizontal)
        {
            var width = ListView.ActualWidth;

            if (!shouldScrollHorizontal)
                width *= -1;

            _InternalListScrollViewer.ChangeView(
                _InternalListScrollViewer.HorizontalOffset + width,
                _InternalListScrollViewer.VerticalOffset,
                _InternalListScrollViewer.ZoomFactor,
                false);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) => AlignSelectedItem();
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) => AlignSelectedItem();
        private void AlignSelectedItem()
        {
            var index = ListView.SelectedIndex;
            if (index < 0)
                return;

            double? itemWidth = ((FrameworkElement)ListView.ContainerFromIndex(index))?.ActualWidth;

            if (itemWidth.HasValue)
            {
                var itemsInView = _InternalListScrollViewer.ActualWidth / itemWidth.Value;
                var topOffset = (itemsInView - 1) * itemWidth.Value / 2;

                _InternalListScrollViewer.ChangeView(
                     (index * itemWidth.Value) - topOffset,
                    _InternalListScrollViewer.VerticalOffset,
                    _InternalListScrollViewer.ZoomFactor,
                    false);
            }
            else if (index != -1)
            {
                //There is an item selected in the ListView, but it isn't rendered yet.
                //Hook up to ChoosingItemContainer event to try again...
                ListView.ChoosingItemContainer += (sender, e) => AlignSelectedItem();
            }
        }
    }
}
