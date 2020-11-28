using AutoMapper;
using Blurhash.UWP;
using Microsoft.Toolkit.Uwp.UI;
using OneSplash.Application.DTOs;
using OneSplash.Domain.Entities;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OneSplash.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SplashPhotoEntity, SplashPhotoDto>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Blurhash, opt => opt.MapFrom(src => src.Blurhash))
                .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => src.ImageUri))
                .ForMember(dest => dest.ImageAuthor, opt => opt.MapFrom(src => src.ImageAuthor))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height));
        }
    }

    public class BlurBrushResolver : IValueResolver<SplashPhotoEntity, SplashPhotoDto, Task<ImageSource>>
    {
        private static readonly Decoder _blurHash = new Decoder();
        private static async Task<ImageSource> GenerateBlurSourceAsync(string blurHash, double width, double height)
        {
            if (string.IsNullOrWhiteSpace(blurHash))
                return default;

            try
            {
                var bitmap = _blurHash.Decode(blurHash, (int)width, (int)height);
                var blurSource = new SoftwareBitmapSource();
                await blurSource.SetBitmapAsync(bitmap);
                return blurSource;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }
        public async Task<ImageSource> Resolve(SplashPhotoEntity source, SplashPhotoDto destination, Task<ImageSource> destMember, ResolutionContext context)
        {
            return await GenerateBlurSourceAsync(source.Blurhash, source.Width / 10, source.Height / 10);
        }
    }

    public class ImageSourceConverter : IValueConverter<string, Task<BitmapImage>>
    {
        public ImageSourceConverter()
        {
            ImageCache.Instance.CacheDuration = TimeSpan.FromHours(24);
            ImageCache.Instance.MaxMemoryCacheCount = 100;
        }
        public async Task<BitmapImage> Convert(string sourceMember, ResolutionContext context)
        {
            try
            {
                await ImageCache.Instance.PreCacheAsync(new Uri(sourceMember));
                return await ImageCache.Instance.GetFromCacheAsync(new Uri(sourceMember));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }
    }
}
