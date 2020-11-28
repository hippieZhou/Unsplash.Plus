using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using OneSplash.Application.DTOs;
using Windows.UI.Xaml.Media.Animation;

namespace OneSplash.UwpApp.Servcies.Messages
{

    public class ConnectedNavMessage : RequestMessage<(SplashPhotoDto selectedItem, ConnectedAnimation animation)>
    {
    }
}
