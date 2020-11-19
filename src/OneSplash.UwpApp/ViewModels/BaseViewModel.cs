using MediatR;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace OneSplash.UwpApp.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= Ioc.Default.GetRequiredService<IMediator>();
    }
}
