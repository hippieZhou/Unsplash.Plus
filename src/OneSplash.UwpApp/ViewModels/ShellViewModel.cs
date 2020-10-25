using Microsoft.Toolkit.Mvvm.Input;
using OneSplash.UwpApp.Common;
using OneSplash.UwpApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.UI;

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
