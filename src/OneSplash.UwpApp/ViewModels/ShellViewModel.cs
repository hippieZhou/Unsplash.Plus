using Microsoft.Toolkit.Mvvm.Input;
using OneSplash.UwpApp.ViewModels.Widgets;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public string SearchWidget { get; } = typeof(SearchWidgetViewModel).FullName;
        public string MoreWidget { get; } = typeof(MoreWidgetViewModel).FullName;

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(() =>
                    {
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
                        //Messenger.Send(new ViewChangedMessage(widgetName));
                    });
                }
                return _navCommand;
            }
        }
    }
}
