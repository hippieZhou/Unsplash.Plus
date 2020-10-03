using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp;
using System;
using System.Windows.Input;
using Unsplash.Plus.Models;
using Unsplash.Plus.Services;

namespace Unsplash.Plus.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IUnsplashService _unsplashService;
        private readonly ILogger<MainViewModel> _logger;

        public MainViewModel(IUnsplashService unsplashService, ILogger<MainViewModel> logger)
        {
            _unsplashService = unsplashService ?? throw new ArgumentNullException(nameof(unsplashService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private IncrementalLoadingCollection<PhotoItemSource, PhotoItem> _items;
        public IncrementalLoadingCollection<PhotoItemSource, PhotoItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new AsyncRelayCommand(async () =>
                    {
                        if (Items == null)
                        {
                            Items = new IncrementalLoadingCollection<PhotoItemSource, PhotoItem>(
                                new PhotoItemSource(_unsplashService),
                                itemsPerPage: 10,
                                onStartLoading: () =>
                                {
                                    IsError = false;
                                    _logger.LogInformation("加载中");
                                },
                                onEndLoading: () =>
                                {
                                    IsError = false;
                                    _logger.LogInformation("加载完毕");
                                },
                                onError: ex =>
                                {
                                    IsError = true;
                                    _logger.LogError(ex, "加载异常");
                                });
                        }
                        await Items.LoadMoreItemsAsync(10);
                    });
                }
                return _loadCommand;
            }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get 
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new AsyncRelayCommand(async () => 
                    {
                        await Items.RefreshAsync();
                    });
                }
                return _refreshCommand; }
        }
    }
}
