using Blurhash.UWP;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using OneSplash.Application.DTOs;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.UwpApp.Common
{
    public class ImageLoader
    {
        private static readonly Decoder _blurHash = new Decoder();

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
                if (d is ImageEx image && e.NewValue is SplashPhotoDto model && model != null)
                {
                    var bgBrush = model.Color.ToColor();
                    image.Background = new SolidColorBrush(bgBrush);

                    if (image.FindAscendant<GridViewItem>() is GridViewItem gridviewItem)
                    {
                        gridviewItem.SizeChanged += async (sender, e) =>
                        {
                            image.PlaceholderSource = await GenerateBlurSourceAsync(model.Blurhash, gridviewItem.Width, gridviewItem.Height);
                        };
                        image.PlaceholderSource = await GenerateBlurSourceAsync(model.Blurhash, gridviewItem.Width, gridviewItem.Height);
                    }
                    image.Source = await ImageCache.Instance.GetFromCacheAsync(new Uri(model.ImageUri));

                    //var imageSource = model.ImageUri;
                    //if (!string.IsNullOrWhiteSpace(imageSource))
                    //{
                    //    var imageUri = new Uri($"ms-appx://{imageSource}", UriKind.RelativeOrAbsolute);
                    //    image.Source = imageUri;

                    //    //var file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);
                    //    //using IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                    //    //BitmapImage bitmapImage = new BitmapImage();
                    //    //await bitmapImage.SetSourceAsync(fileStream);
                    //    //image.Source = bitmapImage;
                    //}
                }
            }));


        private static async System.Threading.Tasks.Task<ImageSource> GenerateBlurSourceAsync(string blurHash,double width, double height)
        {
            try
            {
                var bitmap = _blurHash.Decode(blurHash, (int)width, (int)height);
                var blurSource = new SoftwareBitmapSource();
                await blurSource.SetBitmapAsync(bitmap);
                return blurSource;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }
    }
}
