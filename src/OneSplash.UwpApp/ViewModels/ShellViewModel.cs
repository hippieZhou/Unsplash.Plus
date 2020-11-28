using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.UwpApp.Servcies.Messages;
using OneSplash.UwpApp.Servcies.Navigation;
using OneSplash.UwpApp.ViewModels.Widgets;
using OneSplash.UwpApp.Views;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using WinUi = Microsoft.UI.Xaml.Controls;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private WinUi.NavigationView _mainNav;
        private readonly INavigationService _navService;

        public string SearchWidget { get; } = typeof(SearchWidgetViewModel).FullName;
        public string MoreWidget { get; } = typeof(MoreWidgetViewModel).FullName;

        public ShellViewModel(INavigationService navService)
        {
            _navService = navService ?? throw new ArgumentNullException(nameof(navService));
        }

        public void Initialize(WinUi.NavigationView mainNav, Frame contentFrame)
        {
            _mainNav = mainNav;
            _navService.CurrentFrame = contentFrame;
            _navService.CurrentFrame.Navigated += (sender, e) => 
            {

            };
            _navService.Configure(typeof(MainViewModel).FullName, typeof(MainView));
        }

        private object _selectedNavItem;
        public object SelectedNavItem
        {
            get { return _selectedNavItem; }
            set { SetProperty(ref _selectedNavItem, value); }
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(() =>
                    {
                        _navService.NavigateTo(typeof(MainViewModel).FullName);
                    });
                }
                return _loadCommand;
            }
        }

        private ICommand _navCommand;

        public ICommand NavCommand
        {
            get
            {
                if (_navCommand == null)
                {
                    _navCommand = new RelayCommand<string>(widgetName =>
                    {
                        WeakReferenceMessenger.Default.Send(new ViewChangedMessage(widgetName));
                    });
                }
                return _navCommand;
            }
        }
    }
}
