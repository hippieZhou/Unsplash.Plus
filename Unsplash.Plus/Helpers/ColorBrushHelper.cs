using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Unsplash.Plus.Helpers
{
    public static class ColorBrushHelper
    {
        public static readonly IDictionary<string, Color> Colors = typeof(Colors).GetRuntimeProperties().Select(x => (x.Name, (Color)x.GetValue(null))).ToDictionary(x => x.Name.ToLower(), x => x.Item2);

        public static SolidColorBrush GetSolidColorBrush(string hex)
        {
            try
            {
                var color = hex.ToColor();
                return new SolidColorBrush(color);
            }
            catch (Exception)
            {
                return new SolidColorBrush(Windows.UI.Colors.Black);
            }
        }
    }
}
