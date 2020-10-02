using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Unsplash.Plus.Services;
using Windows.UI.Xaml.Controls;

namespace Unsplash.Plus.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private readonly INavigationService _navService;

        public ShellViewModel(INavigationService navService)
        {
            _navService = navService ?? throw new ArgumentNullException(nameof(navService));
        }
        public void Initialize(Frame contentFrame)
        {
            if (contentFrame == null)
                throw new ArgumentNullException(nameof(contentFrame));
            _navService.Initialize(contentFrame);
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
                        _navService.NavigateTo(nameof(MainViewModel));
                    });
                }
                return _loadCommand;
            }
        }
    }
}
