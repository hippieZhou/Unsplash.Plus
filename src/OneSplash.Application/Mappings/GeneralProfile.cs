using AutoMapper;
using OneSplash.Application.DTOs;
using OneSplash.Domain.Entities;

namespace OneSplash.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SplashPhotoEntity, SplashPhotoDto>().ReverseMap();
        }
    }
}
