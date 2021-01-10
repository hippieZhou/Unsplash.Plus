using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.UwpApp.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class DownloadView : UserControl
    {
        public DownloadViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<DownloadViewModel>();
        public DownloadView()
        {
            this.InitializeComponent();
        }

        private ScalarAnimation CreateScalarAnimation(bool isOpen)
        {
            var root = this.FindAscendant<Page>();
            if (root == null)
                return default;

            var scalarAnimation = new ScalarAnimation
            {
                Target = "Translation.Y",
                Duration = TimeSpan.FromMilliseconds(800)
            };
            if (isOpen)
            {
                scalarAnimation.From = root.RenderSize.Height;
                scalarAnimation.To = 0d;
            }
            else
            {
                scalarAnimation.From = 0d;
                scalarAnimation.To = root.RenderSize.Height;
            }
            return scalarAnimation;
        }

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            var scalarAnimation = CreateScalarAnimation(e.NewState == Visible);
            if (scalarAnimation == null)
                return;
            scalarAnimation.StartAnimation(this);
        }
    }
}
