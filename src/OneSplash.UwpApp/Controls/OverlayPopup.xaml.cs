﻿using OneSplash.Application.DTOs;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class OverlayPopup : UserControl
    {
        public OverlayPopup()
        {
            this.InitializeComponent();
        }

        public SplashPhotoDto SelectedItem
        {
            get { return (SplashPhotoDto)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(SplashPhotoDto), typeof(OverlayPopup), new PropertyMetadata(DependencyProperty.UnsetValue));

        public ICommand HideCommand
        {
            get { return (ICommand)GetValue(HideCommandProperty); }
            set { SetValue(HideCommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for HideCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideCommandProperty =
            DependencyProperty.Register("HideCommand", typeof(ICommand), typeof(OverlayPopup), new PropertyMetadata(DependencyProperty.UnsetValue));

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource.Equals(rootGrid))
            {
                HideCommand?.Execute(SelectedItem);
                e.Handled = true;
            }
        }

        private void DestinationElement_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void DestinationElement_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            destinationElement_Transform.TranslateX += e.Delta.Translation.X;
            destinationElement_Transform.TranslateY += e.Delta.Translation.Y;
        }

        private void DestinationElement_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            DoubleAnimation CreateTranslateAnimation(EasingFunctionBase easingFunction, double from, double to = 0, double duration = 600)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    EasingFunction = easingFunction,
                    From = from,
                    To = to,
                    Duration = new Duration(TimeSpan.FromMilliseconds(duration))
                };
                return animation;
            }

            var ef = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            DoubleAnimation translateXAnimation = CreateTranslateAnimation(ef, destinationElement_Transform.TranslateX);
            DoubleAnimation translateYAnimation = CreateTranslateAnimation(ef, destinationElement_Transform.TranslateY);

            Storyboard.SetTarget(translateXAnimation, destinationElement_Transform);
            Storyboard.SetTarget(translateYAnimation, destinationElement_Transform);
            Storyboard.SetTargetProperty(translateXAnimation, "CompositeTransform.TranslateX");
            Storyboard.SetTargetProperty(translateYAnimation, "CompositeTransform.TranslateY");

            Storyboard sb = new Storyboard();
            sb.Children.Add(translateXAnimation);
            sb.Children.Add(translateYAnimation);
            sb.Begin();
        }
    }
}
