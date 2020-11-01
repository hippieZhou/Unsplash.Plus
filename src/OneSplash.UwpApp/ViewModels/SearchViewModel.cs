using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels
{
    public class SearchViewModel : ObservableRecipient
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
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
                        this.IsActive = true;
                    });
                }
                return _loadCommand;
            }
        }

        protected override void OnActivated()
        {
            Messenger.Register(this, new MessageHandler<object, ValueChangedMessage<Visibility>>((sender, target) =>
            {
                Visibility = target.Value;
            }));
        }
    }
}
