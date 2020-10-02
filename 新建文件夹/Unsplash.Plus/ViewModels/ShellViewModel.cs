using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Unsplash.Plus.ViewModels
{
    public class ShellViewModel : ObservableObject
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
                        Trace.WriteLine(DateTime.Now);
                    });
                }
                return _loadCommand;
            }
        }
    }
}
