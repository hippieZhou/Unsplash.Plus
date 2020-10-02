using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Unsplash.Plus.Services
{
    public interface INavigationService
    {
        string CurrentPage { get; }
        void Initialize(Frame appFrame);
        void Configure(string page, Type type);
        void NavigateTo(string page);
        void NavigateTo(string page, object parameter);
        void GoBack();
    }

    public class NavigationService : INavigationService
    {
        public const string RootPage = "(Root)";
        public const string UnknownPage = "(Unknown)";

        private readonly IDictionary<string, Type> pages_ = new Dictionary<string, Type>();
        private Frame _appFrame;
        public NavigationService() => _appFrame = Window.Current.Content as Frame;

        public string CurrentPage
        {
            get
            {
                var frame = _appFrame;
                if (frame.BackStackDepth == 0)
                    return RootPage;

                if (frame.Content == null)
                    return UnknownPage;

                var type = frame.Content.GetType();

                lock (pages_)
                {
                    if (pages_.Values.All(v => v != type))
                        return UnknownPage;

                    var item = pages_.Single(i => i.Value == type);

                    return item.Key;
                }
            }
        }

        public void Initialize(Frame appFrame)
        {
            _appFrame = appFrame;
        }

        public void Configure(string page, Type type)
        {
            lock (pages_)
            {
                if (pages_.ContainsKey(page))
                    throw new ArgumentException("The specified page is already registered.");

                if (pages_.Values.Any(v => v == type))
                    throw new ArgumentException("The specified view has already been registered under another name.");

                pages_.Add(page, type);
            }
        }

        public void NavigateTo(string page) => NavigateTo(page, null);

        public void NavigateTo(string page, object parameter)
        {
            lock (pages_)
            {
                if (!pages_.ContainsKey(page))
                    throw new ArgumentException("Unable to find a page registered with the specified name.");

                Debug.Assert(_appFrame != null);
                _appFrame.Navigate(pages_[page], parameter);
            }
        }

        public void GoBack()
        {
            Debug.Assert(_appFrame != null);
            if (_appFrame.CanGoBack)
                _appFrame.GoBack();
        }


    }
}
