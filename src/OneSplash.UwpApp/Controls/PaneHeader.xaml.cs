using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace OneSplash.UwpApp.Controls
{
    public sealed partial class PaneHeader : UserControl
    {
        public PaneHeader()
        {
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PaneHeader), new PropertyMetadata(DependencyProperty.UnsetValue));
    }
}
