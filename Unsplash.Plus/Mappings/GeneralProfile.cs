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
        private static async Task<ImageSource> GenerateBlurHash(string blurHash, int width, int height)
        {
            var bitmap = new Decoder().Decode(blurHash, width, height);
            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(bitmap);
            return source;
        }

        public GeneralProfile()
        {
            CreateMap<Unsplasharp.Models.Category, Models.Category>();
            CreateMap<Unsplasharp.Models.CategoryLinks, Models.CategoryLinks>();
            CreateMap<Unsplasharp.Models.Collection, Models.Collection>();
            CreateMap<Unsplasharp.Models.CollectionLinks, Models.CollectionLinks>();
            CreateMap<Unsplasharp.Models.Exif, Models.Exif>();
            CreateMap<Unsplasharp.Models.Location, Models.Location>();
            CreateMap<Unsplasharp.Models.Photo, Models.Photo>();//.ForMember(dest => dest.BlurHash, opt => opt.MapFrom(src => GenerateBlurHash(src.BlurHash, src.Width, src.Height)));
            CreateMap<Unsplasharp.Models.PhotoLinks, Models.PhotoLinks>();
            CreateMap<Unsplasharp.Models.ProfileImage, Models.ProfileImage>();
            CreateMap<Unsplasharp.Models.Urls, Models.Urls>();
            CreateMap<Unsplasharp.Models.User, Models.User>();
        }
    }
}
