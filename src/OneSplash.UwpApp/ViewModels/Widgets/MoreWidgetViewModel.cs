using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using OneSplash.UwpApp.Servcies.Messages;

namespace OneSplash.UwpApp.ViewModels.Widgets
{
    public class MoreWidgetViewModel : BaseWidgetViewModel, IRecipient<ViewChangedMessage>
    {
        public void Receive(ViewChangedMessage message)
        {
            if (message.Value == typeof(MoreWidgetViewModel).FullName)
            {
                Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
