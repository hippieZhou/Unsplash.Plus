using AutoMapper;
using Unsplash.Plus.Models;
using Unsplasharp.Models;

namespace Unsplash.Plus.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<Photo, PhotoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Custom, opt => opt.MapFrom(src => src.Urls.Custom))
                .ForMember(dest => dest.Full, opt => opt.MapFrom(src => src.Urls.Full))
                .ForMember(dest => dest.Raw, opt => opt.MapFrom(src => src.Urls.Raw))
                .ForMember(dest => dest.Regular, opt => opt.MapFrom(src => src.Urls.Regular))
                .ForMember(dest => dest.Small, opt => opt.MapFrom(src => src.Urls.Small))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Urls.Thumbnail));
        }
    }
}
