using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.UwpApp.Extensions;
using System.Linq;
using System.Numerics;
using System.Windows.Input;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class SplashGridView : UserControl
    {
        public SplashGridView()
        {
            this.InitializeComponent();
        }

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object Footer
        {
            get { return (object)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Footer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register("Footer", typeof(object), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public Thickness ItemMargin
        {
            get { return (Thickness)GetValue(ItemMarginProperty); }
            set { SetValue(ItemMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(SplashGridView), new PropertyMetadata(new Thickness(0)));


        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(SplashGridView), new PropertyMetadata(new Thickness(0)));


        public bool ItemLoading
        {
            get { return (bool)GetValue(ItemLoadingProperty); }
            set { SetValue(ItemLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemLoadingProperty =
            DependencyProperty.Register("ItemLoading", typeof(bool), typeof(SplashGridView), new PropertyMetadata(false));

        public double DesiredWidth
        {
            get { return (double)GetValue(DesiredWidthProperty); }
            set { SetValue(DesiredWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesiredWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesiredWidthProperty =
            DependencyProperty.Register("DesiredWidth", typeof(double), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register("ItemClickCommand", typeof(ICommand), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register("LoadCommand", typeof(ICommand), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));


        public ICommand BackToTopCommand
        {
            get { return (ICommand)GetValue(BackToTopCommandProperty); }
            set { SetValue(BackToTopCommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BackToTopCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackToTopCommandProperty =
            DependencyProperty.Register("BackToTopCommand", typeof(ICommand), typeof(SplashGridView), new PropertyMetadata(DependencyProperty.UnsetValue));

        private void OnItemGridViewContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (sender.ContainerFromItem(sender.Items.LastOrDefault()) is GridViewItem container)
            {
                container.XYFocusDown = container;
            }
        }

        private void OnItemGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}
