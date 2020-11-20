using Microsoft.Toolkit.Mvvm.Messaging;
using OneSplash.UwpApp.Servcies.Messages;

namespace OneSplash.UwpApp.ViewModels.Widgets
{
    public class SearchWidgetViewModel : BaseWidgetViewModel, IRecipient<ViewChangedMessage>
    {
        public void Receive(ViewChangedMessage message)
        {
            if (message.Value == typeof(SearchWidgetViewModel).FullName)
            {
                Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
