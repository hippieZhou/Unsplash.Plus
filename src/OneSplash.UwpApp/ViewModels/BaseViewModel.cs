using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= App.Locator.Provider.GetRequiredService<IMediator>();
    }
}
