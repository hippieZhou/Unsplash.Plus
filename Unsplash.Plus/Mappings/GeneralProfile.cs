using AutoMapper;
using Unsplash.Plus.Models;
using Unsplasharp.Models;

namespace Unsplash.Plus.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            //var decoder = new Decoder();
            //async Task<SoftwareBitmapSource> Generate(string blurHash, int width, int height)
            //{
            //    var bitmap = decoder.Decode(blurHash, width, height);
            //    var source = new SoftwareBitmapSource();
            //    await source.SetBitmapAsync(bitmap);
            //    return source;
            //}


            CreateMap<Photo, PhotoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                //.ForMember(dest => dest.BlurHash, opt => opt.MapFrom(src => new NotifyTaskCompletion<SoftwareBitmapSource>(Generate("hash", src.Width, src.Height))))

                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => $"{src.Location.City}/{src.Location.Country}"))

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(src => src.User.ProfileImage.Small));
            CreateMap<Urls, PhotoUrls>().ReverseMap();
            CreateMap<Location, PhotoLocation>().ReverseMap();
        }
    }
}
