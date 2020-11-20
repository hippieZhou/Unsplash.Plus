using MediatR;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace OneSplash.UwpApp.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected IMediator Mediator => Ioc.Default.GetRequiredService<IMediator>();
    }
}
