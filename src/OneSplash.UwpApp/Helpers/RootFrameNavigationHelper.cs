using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Helpers
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public  class RootFrameNavigationHelper
    {
        private readonly Frame _frame;
        private readonly WinUI.NavigationView _currentNavView;

        public IEnumerable<object> MenuItems => _currentNavView.MenuItems;
        public RootFrameNavigationHelper(Frame rootFrame, WinUI.NavigationView currentNavView)
        {
            _frame = rootFrame ?? throw new ArgumentNullException(nameof(rootFrame));
            _currentNavView = currentNavView ?? throw new ArgumentNullException(nameof(currentNavView));

            _frame.Navigated += (sender, e) =>
            {
                UpdateBackButton();
            };
            _currentNavView.BackRequested += (sender, e) => GoForward();
        }

        private void UpdateBackButton() => _currentNavView.IsBackEnabled = _frame.CanGoBack;

        public bool CanGoBack() =>
            (!_currentNavView.IsPaneOpen || _currentNavView.DisplayMode != WinUI.NavigationViewDisplayMode.Compact && this._currentNavView.DisplayMode != WinUI.NavigationViewDisplayMode.Minimal) && _frame.CanGoBack;

        public bool CanGoForward => _frame.CanGoForward;

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }

        public void GoForward()
        {
            if (_frame.CanGoForward)
            {
                _frame.GoForward();
            }
        }


        public void NavigationTo(Type sourcePageType)
        {
            _frame.Navigate(sourcePageType);
        }
    }
}
