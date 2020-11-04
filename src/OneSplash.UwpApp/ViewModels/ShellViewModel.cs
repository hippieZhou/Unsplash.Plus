using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class ShellViewModel : BaseViewModel
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
    }
}
