using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class DownloadViewModel : BaseViewModel
    {
        private bool _isPaneShow;
        public bool IsPaneShow
        {
            get { return _isPaneShow; }
            set { SetProperty(ref _isPaneShow, value); }
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
                        IsPaneShow = false;
                    });
                }
                return _backCommand;
            }
        }

    }
}
