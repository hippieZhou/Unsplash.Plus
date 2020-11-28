using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace OneSplash.UwpApp.Extensions
{
    public static class ElementCompositionExtensions
    {
        public static Visual ElementVisual(this UIElement element) => ElementCompositionPreview.GetElementVisual(element);

        public static Visual AddShadow(this Visual visual, Canvas shadowHost)
        {
            if (shadowHost == null)
                throw new ArgumentNullException(nameof(shadowHost));

            var compositor = visual.Compositor;

            var shadowHostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);

            // Create shadow and add it to the Visual Tree
            var shadow = compositor.CreateDropShadow();
            shadow.Color = Color.FromArgb(255, 75, 75, 80);
            shadow.BlurRadius = 25f;
            var shadowVisual = compositor.CreateSpriteVisual();
            shadowVisual.Shadow = shadow;
            ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

            // Make sure the shadow resizes as its host resizes
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", shadowHostVisual);
            shadowVisual.StartAnimation("Size", bindSizeAnimation);

            // Increase the blurradius as the rectangle is scaled up
            var shadowAnimation = compositor.CreateExpressionAnimation("100 * (source.Scale.X - 1)");
            shadowAnimation.SetReferenceParameter("source", visual);
            shadow.StartAnimation("BlurRadius", shadowAnimation);

            return visual;
        }

        public static Visual AddScaleAnimation(this Visual visual, FrameworkElement self, bool cliped = true, float scaleFactor = 1.1f)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            var compositor = visual.Compositor;
            // Create animation to scale up the rectangle
            var pointerEnteredAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerEnteredAnimation.InsertKeyFrame(1.0f, new Vector3(scaleFactor));

            // Create animation to scale the rectangle back down
            var pointerExitedAnimation = compositor.CreateVector3KeyFrameAnimation();
            pointerExitedAnimation.InsertKeyFrame(1.0f, new Vector3(1.0f));

            self.PointerEntered += (sender, e) =>
            {
                if (cliped)
                {
                    var parent = self.FindAscendant<FrameworkElement>();
                    parent.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, self.ActualWidth, self.ActualHeight) };
                }
                visual.CenterPoint = new Vector3(visual.Size / 2, 0);
                visual.StartAnimation("Scale", pointerEnteredAnimation);
            };
            self.PointerExited += (sender, e) =>
            {
                visual.StartAnimation("Scale", pointerExitedAnimation);
            };

            return visual;
        }
    }
}
