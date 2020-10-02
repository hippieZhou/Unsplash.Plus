using System;
using Unsplash.Plus.Helpers;
using Windows.UI.Xaml.Data;

namespace Unsplash.Plus.Converters
{
    public class HexStringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ColorBrushHelper.GetSolidColorBrush(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
