using Microsoft.Toolkit.Mvvm.Input;
using OneSplash.UwpApp.Helpers;
using OneSplash.UwpApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public RootFrameNavigationHelper RootFrameNavigationHelper { get; private set; }

        public ShellViewModel()
        {
            var navItems = new List<WinUI.NavigationViewItemBase>
            {
                new WinUI.NavigationViewItem { Content = "Home", Icon = new SymbolIcon(Symbol.Home), Tag = typeof(MainView) },
                new WinUI.NavigationViewItemHeader { Content = "Actions" },
                new WinUI.NavigationViewItem { Content = "Favorite", Icon = new SymbolIcon(Symbol.Favorite), Tag = typeof(FavoriteView) },
                new WinUI.NavigationViewItem { Content = "Download", Icon = new SymbolIcon(Symbol.Download), Tag = typeof(DownloadView) }
            };
            NavItems = new ObservableCollection<WinUI.NavigationViewItemBase>(navItems);
        }

        public void Initialize(RootFrameNavigationHelper rootFrameNavigationHelper)
        {
            RootFrameNavigationHelper = rootFrameNavigationHelper ?? throw new ArgumentNullException(nameof(rootFrameNavigationHelper));
        }

        private ObservableCollection<WinUI.NavigationViewItemBase> _navItems;
        public ObservableCollection<WinUI.NavigationViewItemBase> NavItems
        {
            get { return _navItems; }
            set { SetProperty(ref _navItems, value); }
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
                        var defaultNavMenu = NavItems.FirstOrDefault();
                        RootFrameNavigationHelper.NavigationTo((Type)defaultNavMenu.Tag);
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
                        var invokedItem = args.InvokedItemContainer;
                    });
                }
                return _itemInvokedCommand;
            }
        }
    }
}
