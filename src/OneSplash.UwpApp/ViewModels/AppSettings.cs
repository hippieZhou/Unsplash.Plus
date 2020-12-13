using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.Extensions;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace OneSplash.UwpApp.ViewModels
{
    public partial class AppSettings : ObservableObject
    {
        private readonly ApplicationDataContainer _localSettings;
        public AppSettings()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public string DBFile => Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite");
        public string AppDisplayName => "AppDisplayName".GetLocalized();

        public string ApplicationVersion
        {
            get
            {
                var appVer = SystemInformation.Instance.ApplicationVersion;
                return $"{appVer.Major}.{appVer.Minor}.{appVer.Build}.{appVer.Revision}";
            }
        }

        private int _theme;
        public int Theme
        {
            get { return ReadSettings(nameof(Theme), (int)ElementTheme.Default); }
            set
            {
                if (value < (int)ElementTheme.Default)
                {
                    value = (int)ElementTheme.Default;
                }
                if (value > (int)ElementTheme.Dark)
                {
                    value = (int)ElementTheme.Dark;
                }
                SaveSettings(nameof(Theme), value);
                SetProperty(ref _theme, value);
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = (ElementTheme)Enum.ToObject(typeof(ElementTheme), value);
                }
            }
        }
    }

    public partial class AppSettings : ObservableObject
    {
        public async Task<StorageFolder> GetSavingFolderAsync() => await KnownFolders.PicturesLibrary.CreateFolderAsync("Attention", CreationCollisionOption.OpenIfExists);

        public async Task<StorageFolder> GetTemporaryFolderAsync() => await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("ImageCache", CreationCollisionOption.OpenIfExists);

        public async Task<StorageFolder> GetLogFolderAsync() => await ApplicationData.Current.LocalFolder.CreateFolderAsync("Logs", CreationCollisionOption.OpenIfExists);


        private void SaveSettings(string key, object value)
        {
            _localSettings.Values[key] = value;
        }
        private T ReadSettings<T>(string key, T defaultValue)
        {
            if (_localSettings.Values.ContainsKey(key))
            {
                return (T)_localSettings.Values[key];
            }
            if (defaultValue != null)
            {
                return defaultValue;
            }
            return default;
        }

    }
}
