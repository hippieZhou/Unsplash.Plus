using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class DownloadViewModel : BaseViewModel
    {
        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set { SetProperty(ref _isOpen, value); }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(() =>
                    {
                        IsOpen = false;
                    });
                }
                return _backCommand;
            }
        }

    }
}
