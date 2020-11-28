using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.Application.DTOs
{
    public class SplashPhotoDto
    {
        public string Color { get; set; }
        public string Blurhash { get; set; }
        public string ImageUri { get; set; }
        public string ImageAuthor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
