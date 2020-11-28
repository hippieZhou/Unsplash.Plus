using OneSplash.Application.DTOs;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class OverlayPopup : UserControl
    {
        public OverlayPopup()
        {
            this.InitializeComponent();
        }

        public SplashPhotoDto SelectedItem
        {
            get { return (SplashPhotoDto)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(SplashPhotoDto), typeof(OverlayPopup), new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand HideCommand
        {
            get { return (ICommand)GetValue(HideCommandProperty); }
            set { SetValue(HideCommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for HideCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideCommandProperty =
            DependencyProperty.Register("HideCommand", typeof(ICommand), typeof(OverlayPopup), new PropertyMetadata(DependencyProperty.UnsetValue));

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.Equals(rootGrid))
            {
                HideCommand?.Execute(SelectedItem);
                e.Handled = true;
            }
        }
    }
}
