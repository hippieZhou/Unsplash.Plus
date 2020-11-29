using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class PaneFooter : UserControl
    {
        public PaneFooter()
        {
            this.InitializeComponent();
        }

        public string ApplicationVersion
        {
            get
            {
                var appVer = SystemInformation.Instance.ApplicationVersion;
                return $"{appVer.Major}.{appVer.Major}.{appVer.Build}.{appVer.Revision}";
            }
        }
    }
}
