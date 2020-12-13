using Microsoft.Toolkit.Uwp.Helpers;

namespace OneSplash.UwpApp.ViewModels
{
    public class MoreViewModel: BaseViewModel
    {
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
