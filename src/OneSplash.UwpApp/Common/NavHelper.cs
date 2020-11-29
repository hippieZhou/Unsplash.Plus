using System;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.Common
{
    public class NavHelper
    {
        public static Type GetNavTo(DependencyObject obj)
        {
            return (Type)obj.GetValue(NavToProperty);
        }

        public static void SetNavTo(DependencyObject obj, Type value)
        {
            obj.SetValue(NavToProperty, value);
        }

        // Using a DependencyProperty as the backing store for NavTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavToProperty =
            DependencyProperty.RegisterAttached("NavTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(default));
    }
}
