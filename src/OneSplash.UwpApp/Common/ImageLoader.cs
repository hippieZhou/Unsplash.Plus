using Blurhash.UWP;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OneSplash.UwpApp.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.UwpApp.Common
{
    public class ImageLoader
    {
        private static readonly Decoder _blurHash = new Decoder();

        public static Splash GetSource(DependencyObject obj)
        {
            return (Splash)obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, Splash value)
        {
            obj.SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(Splash), typeof(ImageLoader), new PropertyMetadata(default, async (d,e)=> 
            {
                if (d is ImageEx image )
                {
                    image.Loaded += async (sender, _e) =>
                    {
                        if (e.NewValue is Splash model && model != null)
                        {
                            var blurHash = model.Blurhash;
                            if (!string.IsNullOrWhiteSpace(blurHash))
                            {
                                var bitmap = _blurHash.Decode(blurHash, (int)image.ActualWidth, (int)image.ActualHeight);
                                var blurSource = new SoftwareBitmapSource();
                                await blurSource.SetBitmapAsync(bitmap);
                                image.PlaceholderSource = blurSource;
                            }

                            var imageSource = model.ImageUri;
                            if (!string.IsNullOrWhiteSpace(imageSource))
                            {
                                var imageUri = new Uri($"ms-appx://{imageSource}", UriKind.RelativeOrAbsolute);
                                image.Source = imageUri;

                                //var file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);
                                //using IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                                //BitmapImage bitmapImage = new BitmapImage();
                                //await bitmapImage.SetSourceAsync(fileStream);
                                //image.Source = bitmapImage;
                            }
                        }
                    };
                }
            }));
    }
}
