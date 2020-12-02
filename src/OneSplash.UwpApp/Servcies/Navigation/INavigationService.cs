using System;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Servcies.Navigation
{
    public interface INavigationService
    {
        string CurrentPage { get; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        void GoBack();
        void GoForward();
        void NavigateTo(string pageKey);
        void NavigateTo(string pageKey, object parameter);
    }
}
