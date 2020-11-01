using OneSplash.UwpApp.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class OverlayPopup : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<object> HandleBack;
        public OverlayPopup()
        {
            this.InitializeComponent();
            this.Back.Click += (sender, e) =>
            {
                HandleBack?.Invoke(sender, e);
            };
        }

        public Splash SelectedItem
        {
            get { return (Splash)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); RaisePropertyChanged(); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Splash), typeof(OverlayPopup), new PropertyMetadata(DependencyProperty.UnsetValue));

        private void RaisePropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnContentViewModeChanged(Microsoft.UI.Xaml.Controls.TwoPaneView sender, object args)
        {

        }
    }
}
