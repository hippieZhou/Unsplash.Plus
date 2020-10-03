using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Unsplash.Plus
{
    public sealed partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect;
        private SplashScreen splash;
        internal bool dismissed = false;
        internal Frame rootFrame;
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();
            splash = splashscreen;
            if (splash != null)
            {
                splash.Dismissed += (sender, e) =>
                {
                    dismissed = true;
                };

                splashImageRect = splash.ImageLocation;
                PositionImage();
                PositionRing();
            }


            Window.Current.SizeChanged += (sender, e) =>
            {
                if (splash != null)
                {
                    splashImageRect = splash.ImageLocation;
                    PositionImage();
                    PositionRing();
                }
            };

            rootFrame = new Frame();

            this.Loaded += async (sender, e) =>
            {
                await RestoreStateAsync(loadState);
            };
        }

        private async Task RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {
                // TODO: write code to load state
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
            DismissExtendedSplash();
        }

        // Position the extended splash screen image in the same location as the system splash screen image.
        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;

        }

        private void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        private void DismissExtendedSplash()
        {
            rootFrame.Navigate(typeof(Shell));
            Window.Current.Content = rootFrame;
        }
    }
}
