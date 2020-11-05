using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp;
using OneSplash.Application.DTOs;
using OneSplash.Application.Features.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private List<CategoryDto> _categories;
        public List<CategoryDto> Categories
        {
            get { return _categories ??= new List<CategoryDto>(); }
            set { SetProperty(ref _categories, value); }
        }

        private IncrementalLoadingCollection<SplashSource, SplashPhotoDto> _filteredRecipeData;
        public IncrementalLoadingCollection<SplashSource, SplashPhotoDto> FilteredRecipeData
        {
            get { return _filteredRecipeData; }
            set { SetProperty(ref _filteredRecipeData, value); }
        }

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
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
                        FilteredRecipeData = new IncrementalLoadingCollection<SplashSource, SplashPhotoDto>(
                            source: new SplashSource(Mediator),
                            itemsPerPage: 10,
                            onStartLoading: () =>
                            {
                                IsError = false;
                                _logger.LogInformation("onStartLoading");
                            },
                            onEndLoading: () =>
                            {
                                IsError = false;
                                _logger.LogInformation("onEndLoading");
                            },
                            onError: ex =>
                            {
                                IsError = true;
                                _logger.LogInformation("onError");
                            });

                        var response = await Mediator.Send(new GetAllCategoryQuery());
                        if (response.Succeeded)
                        {
                            Categories.Clear();
                            Categories.AddRange(response.Data);
                        }
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
