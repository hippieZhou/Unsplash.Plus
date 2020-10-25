using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp;
using OneSplash.UwpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;

namespace OneSplash.UwpApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IncrementalLoadingCollection<RecipeSource, Recipe> _filteredRecipeData;
        public IncrementalLoadingCollection<RecipeSource, Recipe> FilteredRecipeData
        {
            get { return _filteredRecipeData; }
            set { SetProperty(ref _filteredRecipeData, value); }
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
                        FilteredRecipeData = new IncrementalLoadingCollection<RecipeSource, Recipe>(
                            itemsPerPage: 10,
                            onStartLoading: () =>
                            {
                                _logger.LogInformation("onStartLoading");
                            },
                            onEndLoading: () =>
                            {
                                _logger.LogInformation("onEndLoading");
                            },
                            onError: ex =>
                            {
                                _logger.LogInformation("onError");
                            });
                    });
                }
                return _loadCommand;
            }
        }
    }

    public class RecipeSource : IIncrementalSource<Recipe>
    {
        private readonly List<Recipe> _pecipe;

        public RecipeSource()
        {
            _pecipe = GetRecipeList();
        }

        public async Task<IEnumerable<Recipe>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var result = (from p in _pecipe select p).Skip(pageIndex * pageSize).Take(pageSize);
            await Task.Delay(10);
            return result;
        }

        public static List<Recipe> GetRecipeList(int count = 1000)
        {
            // Initialize list of recipes for varied image size layout sample
            var rnd = new Random();
            List<Recipe> tempList = new List<Recipe>(
                                        Enumerable.Range(0, count).Select(k =>
                                            new Recipe
                                            {
                                                Name = "Recipe " + k.ToString(),
                                                Color = GetColors().ElementAt((k % 100) + 1),
                                                Blurhash = GetBlurhashs().ElementAt(rnd.Next(0, 4))
                                            }));
            return tempList;
        }

        private static IEnumerable<string> GetColors()
        {
            var colors = (typeof(Colors).GetRuntimeProperties().Select(c => c.ToString())).ToList();
            for (int i = 0; i < colors.Count(); i++)
            {
                colors[i] = colors[i].Substring(17);
            }
            return colors;
        }

        private static IEnumerable<string> GetBlurhashs()
        {
            var blurHash = new List<string>
            {
                "LEHV6nWB2yk8pyo0adR*.7kCMdnj",
                "LFC$yHwc8^$yIAS$%M%00KxukYIp",
                "LoC%a7IoIVxZ_NM|M{s:%hRjWAo0",
                "LFC$yHwc8^$yIAS$%M%00KxukYIp",
            };
            return blurHash;
        }
    }

}
