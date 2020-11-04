using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Extensions
{
    public static class UIExtensions
    {
        public static TChild FindVisualChild<TChild>(this DependencyObject obj) where TChild : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TChild found)
                    return found;
                else
                {
                    TChild childOfChild = FindVisualChild<TChild>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static async Task StartVisibleAnimationAsync(this UIElement uIElement)
        {
            var page = uIElement.FindAscendant<Page>();
            await uIElement
                .Fade(1.0f)
                .Scale(scaleX: 1.0f, scaleY: 1.0f, centerX: (float)page.ActualWidth / 2, centerY: (float)page.ActualHeight / 2)
                .StartAsync();
        }

        public static async Task StartCollapsedAnimationAsync(this UIElement uIElement)
        {
            var page = uIElement.FindAscendant<Page>();
            await uIElement
                .Fade(0.95f)
                .Scale(scaleX: 0.95f, scaleY: 0.95f, centerX: (float)page.ActualWidth / 2, centerY: (float)page.ActualHeight / 2)
                .StartAsync();
        }
    }
}
