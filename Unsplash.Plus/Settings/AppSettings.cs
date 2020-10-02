using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Unsplash.Plus.Settings
{
    /// <summary>
    /// https://edi.wang/post/2017/9/8/uwp-read-write-settings
    /// </summary>
    public class AppSettings : INotifyPropertyChanged
    {
        private readonly ApplicationDataContainer _localSettings;
        public AppSettings() => _localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 主题设置
        /// </summary>
        public ElementTheme ThemeSetting
        {
            get
            {
                return (ElementTheme)ReadSettings(nameof(ThemeSetting), (int)ElementTheme.Light);
            }
            set
            {
                SaveSettings(nameof(ThemeSetting), (int)value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 语言设置
        /// </summary>
        public string LangSetting
        {
            get { return ReadSettings(nameof(LangSetting), "zh-cn"); }
            set
            {
                SaveSettings(nameof(LangSetting), value);
                NotifyPropertyChanged();
            }
        }

        private void SaveSettings(string key, object value) => _localSettings.Values[key] = value;

        private T ReadSettings<T>(string key, T defaultValue)
        {
            if (_localSettings.Values.ContainsKey(key))
            {
                return (T)_localSettings.Values[key];
            }
            if (null != defaultValue)
            {
                return defaultValue;
            }
            return default;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
