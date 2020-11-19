using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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

        private ICommand _showCommand;
        public virtual ICommand ShowCommand
        {
            get
            {
                if (_showCommand == null)
                {
                    _showCommand = new RelayCommand<bool>(show =>
                    {
                        Visibility = show ? Visibility.Visible : Visibility.Collapsed;
                    });
                }
                return _loadCommand;
            }
        }


        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

    }
}
