using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Common
{
    public static class ViewLoader
    {
        public static bool GetIsShow(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowProperty);
        }

        public static void SetIsShow(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.RegisterAttached("IsShow", typeof(bool), typeof(ViewLoader), new PropertyMetadata(default, (d, e) =>
             {
                 if (d is UserControl handler)
                 {
                     var root = handler.FindParent<Page>();
                     var anim = new TranslationAnimation() { From = "0,0,0", To = $"0,{ (float)root.ActualHeight},0", Duration = TimeSpan.FromSeconds(1), SetInitialValueBeforeDelay = true };
                     anim.StartAnimation(handler);
                    //await handler.Offset(offsetX: 0.0f, offsetY: (float)root.ActualHeight, duration: 10, delay: 0).StartAsync();
                }
             }));
    }
}
