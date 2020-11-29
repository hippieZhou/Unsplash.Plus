using OneSplash.UwpApp.Extensions;
using System;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.Helpers
{
    public static class ThemeHelper
    {
        private const string SelectedAppThemeKey = "SelectedAppTheme";
        private static Window CurrentApplicationWindow;
        private static UISettings uiSettings;
        public static ElementTheme ActualTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    if (rootElement.RequestedTheme != ElementTheme.Default)
                    {
                        return rootElement.RequestedTheme;
                    }
                }
                return App.Current.RequestedTheme.ToString().GetEnum<ElementTheme>();
            }
        }

        public static ElementTheme RootTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }

                return ElementTheme.Default;
            }
            set
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }

                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
                UpdateSystemCaptionButtonColors();
            }
        }

        public static void Initialize()
        {
            CurrentApplicationWindow = Window.Current;
            string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

            if (savedTheme != null)
            {
                RootTheme = savedTheme.GetEnum<ElementTheme>();
            }

            uiSettings = new UISettings();
            uiSettings.ColorValuesChanged += async (sender, e) =>
            {
                if (CurrentApplicationWindow != null)
                {
                    await CurrentApplicationWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        UpdateSystemCaptionButtonColors();
                    });
                }
            };
        }

        public static bool IsDarkTheme() => RootTheme == ElementTheme.Default ? App.Current.RequestedTheme == ApplicationTheme.Dark : RootTheme == ElementTheme.Dark;

        public static void UpdateSystemCaptionButtonColors()
        {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonForegroundColor = IsDarkTheme() ? (Color?)Colors.White : (Color?)Colors.Black;
        }
    }
}
