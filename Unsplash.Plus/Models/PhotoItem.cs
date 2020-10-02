using Windows.UI.Xaml.Media;

namespace Unsplash.Plus.Models
{
    public class PhotoItem
    {
        public string Id { get; set; }
        public string Color { get; set; }

        #region Urls
        public string Custom { get; set; }
        public string Full { get; set; }
        public string Raw { get; set; }
        public string Regular { get; set; }
        public string Small { get; set; }
        public string Thumbnail { get; set; }
        #endregion
    }
}
