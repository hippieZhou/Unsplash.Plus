using OneSplash.Domain.Common;

namespace OneSplash.Domain.Entities
{
    public class SplashPhotoEntity : AuditableBaseEntity
    {
        public string Color { get; set; }
        public string Blurhash { get; set; }
        public string ImageUri { get; set; }
        public string ImageAuthor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
