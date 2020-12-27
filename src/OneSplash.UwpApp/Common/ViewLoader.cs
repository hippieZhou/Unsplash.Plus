using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Common
{
    public static class ViewLoader
    {
        public static bool GetIsOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(ViewLoader), new PropertyMetadata(DependencyProperty.UnsetValue, async (d,e)=> 
            {
                if (d is FrameworkElement handler && e.NewValue is bool open)
                {
                    var root = handler.FindParent<Page>();

                    var contentHeight = root.RenderSize.Height;
                    var contentWidth = root.RenderSize.Width;

                    await handler.Offset(offsetX: 0.0f, offsetY: (float)root.RenderSize.Height).StartAsync();
                    //ScalarAnimation scalarAnimation = new ScalarAnimation { Target= "Translation.Y", From=0.0d,To =  }
                }
            }));
    }
}
