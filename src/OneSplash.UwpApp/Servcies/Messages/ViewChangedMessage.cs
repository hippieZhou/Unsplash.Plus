using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace OneSplash.UwpApp.Servcies.Messages
{
    public class ViewChangedMessage : ValueChangedMessage<string>
    {
        public ViewChangedMessage(string value) : base(value)
        {
        }
    }
}
