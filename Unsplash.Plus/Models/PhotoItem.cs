using Unsplash.Plus.Helpers;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Unsplash.Plus.Models
{
    public class PhotoItem
    {
        public string Id { get; set; }
        public string Color { get; set; }
        public NotifyTaskCompletion<SoftwareBitmapSource> BlurHash { get; set; }

        public PhotoLocation Location { get; set; }
        public string Description { get; set; }
        public UserItem User { get; set; }
        public PhotoUrls Urls { get; set; }
    }
}
