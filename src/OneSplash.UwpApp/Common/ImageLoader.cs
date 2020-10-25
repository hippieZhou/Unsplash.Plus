using Blurhash.UWP;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.UwpApp.Common
{
    public class ImageLoader
    {
        private static readonly Decoder _blurHash = new Decoder();

        public static string GetSource(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, string value)
        {
            obj.SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(string), typeof(ImageLoader), new PropertyMetadata(string.Empty, (d, e) =>
            {
                if (d is ImageEx image)
                {
                    //var item = await ControlInfoDataSource.Instance.GetItemAsync(e.NewValue?.ToString());
                    //if (item?.ImagePath != null)
                    //{
                    //    Uri imageUri = new Uri(item.ImagePath, UriKind.Absolute);
                    //    BitmapImage imageBitmap = new BitmapImage(imageUri);
                    //    image.Source = imageBitmap;
                    //}
                }
            }));

        public static string GetBlurhash(DependencyObject obj)
        {
            return (string)obj.GetValue(BlurhashProperty);
        }

        public static void SetBlurhash(DependencyObject obj, string value)
        {
            obj.SetValue(BlurhashProperty, value);
        }

        // Using a DependencyProperty as the backing store for Blurhash.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlurhashProperty =
            DependencyProperty.RegisterAttached("Blurhash", typeof(string), typeof(ImageLoader), new PropertyMetadata(string.Empty, async (d,e)=> 
            {
                if (d is ImageEx image)
                {
                    var blurhash = e.NewValue?.ToString() ?? "LEHV6nWB2yk8pyo0adR*.7kCMdnj";
                    var bitmap = _blurHash.Decode(blurhash, 269, 173);
                    var source = new SoftwareBitmapSource();
                    await source.SetBitmapAsync(bitmap);
                    image.Source = source;
                }
            }));


    }
}
