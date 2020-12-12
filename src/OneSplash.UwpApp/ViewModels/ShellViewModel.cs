using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private bool _isPaneOpen;
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { SetProperty(ref _isPaneOpen, value); }
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
                    });
                }
                return _loadCommand;
            }
        }
    }
}
