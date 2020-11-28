using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp;
using OneSplash.Application.DTOs;
using OneSplash.Application.Features.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneSplash.UwpApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ObservableCollection<BingPhotoDto> BingPhotos { get; private set; } = new ObservableCollection<BingPhotoDto>();

        private SplashPhotoDto _todaySplash;
        public SplashPhotoDto TodaySplash
        {
            get { return _todaySplash; }
            set { SetProperty(ref _todaySplash, value); }
        }

        private IncrementalLoadingCollection<SplashSource, SplashPhotoDto> _splashPhotos;
        public IncrementalLoadingCollection<SplashSource, SplashPhotoDto> SplashPhotos
        {
            get { return _splashPhotos; }
            set { SetProperty(ref _splashPhotos, value); }
        }

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private SplashPhotoDto _selected;
        public SplashPhotoDto Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(async () =>
                    {
                        var todayResponse = await Mediator.Send(new GetTodaySplashQuery());
                        if (todayResponse.Succeeded)
                        {
                            TodaySplash = todayResponse.Data;
                        }

                        SplashPhotos = new IncrementalLoadingCollection<SplashSource, SplashPhotoDto>(
                            source: new SplashSource(Mediator),
                            itemsPerPage: 10,
                            onStartLoading: () =>
                            {
                                IsError = false;
                            },
                            onEndLoading: () =>
                            {
                                IsError = false;
                            },
                            onError: ex =>
                            {
                                IsError = true;
                                _logger.Log(LogLevel.Error, ex, default, default);
                            });
                    });
                }
                return _loadCommand;
            }
        }
    }

    public class SplashSource : IIncrementalSource<SplashPhotoDto>
    {
        private readonly IMediator _mediator;
        public SplashSource(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEnumerable<SplashPhotoDto>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var parameter = new GetPagedSplashsQuery { PageNumber = pageIndex, PageSize = pageSize };
            var response = await _mediator.Send(parameter, cancellationToken);
            return response.Succeeded ? response.Data : Array.Empty<SplashPhotoDto>();
        }
    }
}
