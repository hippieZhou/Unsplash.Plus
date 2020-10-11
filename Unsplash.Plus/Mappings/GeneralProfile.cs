using AutoMapper;
using Blurhash.UWP;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Unsplash.Plus.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<Unsplasharp.Models.Category, Models.Category>();
            CreateMap<Unsplasharp.Models.CategoryLinks, Models.CategoryLinks>();
            CreateMap<Unsplasharp.Models.Collection, Models.Collection>();
            CreateMap<Unsplasharp.Models.CollectionLinks, Models.CollectionLinks>();
            CreateMap<Unsplasharp.Models.Exif, Models.Exif>();
            CreateMap<Unsplasharp.Models.Location, Models.Location>();
            CreateMap<Unsplasharp.Models.Photo, Models.Photo>().ForMember(dest => dest.BlurHash, opt => opt.Ignore());
            CreateMap<Unsplasharp.Models.PhotoLinks, Models.PhotoLinks>();
            CreateMap<Unsplasharp.Models.ProfileImage, Models.ProfileImage>();
            CreateMap<Unsplasharp.Models.Urls, Models.Urls>();
            CreateMap<Unsplasharp.Models.User, Models.User>();
        }
    }

    public class BlurHashResolver : IValueResolver<Unsplasharp.Models.Photo, Models.Photo, Task<ImageSource>>
    {
        public static async Task<ImageSource> GenerateBlurHash(string blurHash , int width, int height)
        {
            var bitmap = new Decoder().Decode(blurHash, (int)(width * 0.2), (int)(height * 0.2));
            var bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(bitmap);
            return bitmapSource;
        }

        public async Task<ImageSource> Resolve(
            Unsplasharp.Models.Photo source,
            Models.Photo destination,
            Task<ImageSource> destMember,
            ResolutionContext context)
        {
            var bitmapSource = await GenerateBlurHash(source.BlurHash ?? "LEHV6nWB2yk8pyo0adR*.7kCMdnj", source.Width, source.Height);
            return bitmapSource;
        }
    }
}
