using Microsoft.Toolkit.Mvvm.Input;
using OneSplash.UwpApp.Common;
using OneSplash.UwpApp.Servcies.Navigation;
using System;
using System.Linq;
using System.Windows.Input;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private INavigationService _navService;
        private WinUI.NavigationView _navView;

        public void Initialize(Windows.UI.Xaml.Controls.Frame contentFrame, WinUI.NavigationView navView)
        {
            _navService = new NavigationService(contentFrame, navigated: args =>
            {
                 var currentMenu =_navView.MenuItems.OfType<WinUI.NavigationViewItem>().FirstOrDefault(x=> NavigationSelector.GetNavTo(x) == _navService.CurrentPage);
                if (currentMenu != null)
                {
                    currentMenu.IsSelected = true;
                }
                _navView.IsBackEnabled = _navService.CanGoBack;
            });

            _navView = navView ?? throw new ArgumentNullException(nameof(navView));
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
                        var defaultMenu = _navView.MenuItems.OfType<WinUI.NavigationViewItem>().FirstOrDefault();
                        if (defaultMenu != null)
                        {
                            var defalutPage = NavigationSelector.GetNavTo(defaultMenu);
                            _navService.NavigateTo(defalutPage);
                        }
                    });
                }
                return _loadCommand;
            }
        }

        private ICommand _itemInvokedCommand;
        public ICommand ItemInvokedCommand
        {
            get
            {
                if (_itemInvokedCommand == null)
                {
                    _itemInvokedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(args =>
                    {
                        if (args.InvokedItemContainer.IsSelected || args.IsSettingsInvoked)
                        {
                            return;
                        }

                        if (args.InvokedItemContainer is WinUI.NavigationViewItem navItem)
                        {
                            var page = NavigationSelector.GetNavTo(navItem);
                            _navService.NavigateTo(page);
                        }
                    });
                }
                return _itemInvokedCommand;
            }
        }

        private ICommand _backRequestedCommand;
        public ICommand BackRequestedCommand
        {
            get
            {
                if (_backRequestedCommand == null)
                {
                    _backRequestedCommand = new RelayCommand<WinUI.NavigationViewBackRequestedEventArgs>(args =>
                    {
                        _navService.GoBack();
                    });
                }
                return _backRequestedCommand;
            }
        }
    }
}
