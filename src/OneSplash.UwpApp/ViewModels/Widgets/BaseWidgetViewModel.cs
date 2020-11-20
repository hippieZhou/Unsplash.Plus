using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.UwpApp.Servcies.Messages;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels.Widgets
{
    public abstract class BaseWidgetViewModel : ObservableRecipient
    {
        private Visibility _visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        private ICommand _loadCommand;
        public virtual ICommand LoadCommand
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

        private ICommand _hideCommand;
        public virtual ICommand HideCommand
        {
            get
            {
                if (_hideCommand == null)
                {
                    _hideCommand = new RelayCommand(() =>
                    {
                        Visibility = Visibility.Collapsed;
                    });
                }
                return _hideCommand;
            }
        }


        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ViewChangedMessage>(this, (r, m) =>
            {
                Visibility = GetSelfType() == m.Value ? Visibility.Visible : Visibility.Collapsed;
            });
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.Unregister<ViewChangedMessage>(this);
            base.OnDeactivated();
        }

        public abstract string GetSelfType();
    }
}
