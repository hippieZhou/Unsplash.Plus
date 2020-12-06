using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace OneSplash.UwpApp.Controls
{
    public sealed partial class SplashGridViewFooter : UserControl
    {
        public SplashGridViewFooter()
        {
            this.InitializeComponent();
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(SplashGridViewFooter), new PropertyMetadata(true));

        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.Register("IsError", typeof(bool), typeof(SplashGridViewFooter), new PropertyMetadata(false));

    }
}
