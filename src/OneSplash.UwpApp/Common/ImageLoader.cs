using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OneSplash.Application.DTOs;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Common
{
    public class ImageLoader
    {
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

                    try
                    {
                        image.Source = await ImageCache.Instance.GetFromCacheAsync(new Uri(model.ImageUri));
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.ToString());
                    }
                }
            }));
    }
}
