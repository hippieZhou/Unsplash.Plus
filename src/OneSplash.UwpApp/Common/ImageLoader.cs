using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OneSplash.Application.DTOs;
using OneSplash.UwpApp.Extensions;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.UwpApp.Common
{
    public static class ImageLoader
    {
        private static async Task<ImageSource> FromCachedFileAsync(string imageUri)
        {
            return await ImageCache.Instance.GetFromCacheAsync(new Uri(imageUri));
        }

        private static async Task<ImageSource> FromStorageFileAsync(string imageUri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx://{imageUri}", UriKind.RelativeOrAbsolute));
            using var randomAccessStream = await file.OpenAsync(FileAccessMode.Read);
            var imageSource = new BitmapImage();
            await imageSource.SetSourceAsync(randomAccessStream);
            return imageSource;
        }

        public static SplashPhotoDto GetSource(DependencyObject obj)
        {
            return (SplashPhotoDto)obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, SplashPhotoDto value)
        {
            obj.SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(SplashPhotoDto), typeof(ImageLoader), new PropertyMetadata(default, async (d, e) =>
            {
                if (d is FrameworkElement rootGrid && e.NewValue is SplashPhotoDto model && model != null)
                {
                    var imageBrush = model.Color.ToColor();
                    var imageSource = await FromCachedFileAsync(model.ImageUri);

                    if (rootGrid.FindName("itemSplash") is ImageEx imageEx && imageSource != null)
                    {
                        imageEx.Loaded += (sender, args) =>
                        {
                            imageEx.ElementVisual().AddScaleAnimation(imageEx);
                        };

                        imageEx.Background = new SolidColorBrush(imageBrush);
                        imageEx.Source = imageSource;
                    }
                }
            }));
    }
}
