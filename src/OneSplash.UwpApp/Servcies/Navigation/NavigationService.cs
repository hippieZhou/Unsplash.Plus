using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OneSplash.UwpApp.Servcies.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Frame _currentFrame;

        public NavigationService(
            Frame rootFrame,
            Action<NavigationEventArgs> navigated = null,
            Action<NavigatingCancelEventArgs> navigating = null,
            Action<NavigationFailedEventArgs> navigationFailed = null,
            Action<NavigationEventArgs> navigationStopped = null)
        {
            _currentFrame = rootFrame ?? throw new ArgumentNullException(nameof(rootFrame));
            _currentFrame.Navigated += (sender, e) => navigated?.Invoke(e);
            _currentFrame.Navigating += (sender, e) => navigating?.Invoke(e);
            _currentFrame.NavigationFailed += (sender, e) => navigationFailed?.Invoke(e);
            _currentFrame.NavigationStopped += (sender, e) => navigationStopped?.Invoke(e);
        }

        public string CurrentPage => _currentFrame.CurrentSourcePageType.FullName;
        public bool CanGoBack => _currentFrame.CanGoBack;
        public bool CanGoForward => _currentFrame.CanGoForward;

        public void GoBack()
        {
            if (_currentFrame.CanGoBack)
            {
                _currentFrame.GoBack();
            }
        }

        public void GoForward()
        {
            if (_currentFrame.CanGoForward)
            {
                _currentFrame.GoForward();
            }
        }

        public void NavigateTo(string pageKey)
        {
            _currentFrame.Navigate(Type.GetType(pageKey));
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            _currentFrame.Navigate(Type.GetType(pageKey), parameter);
        }
    }
}
