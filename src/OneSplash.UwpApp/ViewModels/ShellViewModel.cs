using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : ObservableRecipient
    {
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

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(() =>
                    {
                        Messenger.Send(new ValueChangedMessage<Visibility>(Visibility.Visible), nameof(SearchViewModel));
                    });
                }
                return _searchCommand;
            }
        }

        private ICommand _infoCommand;
        public ICommand InfoCommand
        {
            get
            {
                if (_infoCommand == null)
                {
                    _infoCommand = new RelayCommand(() =>
                    {
                        Messenger.Send(new ValueChangedMessage<Visibility>(Visibility.Visible), nameof(InfoViewModel));
                    });
                }
                return _infoCommand;
            }
        }
    }
}
