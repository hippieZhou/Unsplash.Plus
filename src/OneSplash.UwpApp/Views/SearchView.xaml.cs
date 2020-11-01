﻿using Microsoft.Extensions.DependencyInjection;
using OneSplash.UwpApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Views
{
    public sealed partial class SearchView : UserControl
    {
        public SearchViewModel ViewModel { get; } = App.Locator.Provider.GetRequiredService<SearchViewModel>();
        public SearchView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
