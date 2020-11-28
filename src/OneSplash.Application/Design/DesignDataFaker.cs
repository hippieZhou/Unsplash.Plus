using Bogus;
using OneSplash.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI;

namespace OneSplash.Application.Design
{
    public static class DesignDataFaker
    {
        private static readonly Faker<SplashPhotoDto> _testOrders;

        static DesignDataFaker()
        {
            _testOrders = new Faker<SplashPhotoDto>()
                .StrictMode(true)
                .RuleFor(x => x.ImageAuthor, f => f.Internet.UserName())
                .RuleFor(x => x.Color, f => f.PickRandom(GetColors()))
                .RuleFor(x => x.ImageUri, f => f.Image.PicsumUrl())
                .RuleFor(x => x.Width, f => f.Random.Int())
                .RuleFor(x => x.Height, f => f.Random.Int())
                .RuleFor(x => x.Blurhash, f => f.PickRandom(GetBlurhashs()));
        }

        public static async Task<SplashPhotoDto> GenerateSplashPhotoDtoAsync() => await Task.FromResult(_testOrders.Generate());

        public static async Task<IEnumerable<SplashPhotoDto>> GenerateSplashPhotosAsync(int count = 10) => await Task.FromResult(_testOrders.Generate(count));

        private static IEnumerable<string> GetColors()
        {
            var colors = (typeof(Colors).GetRuntimeProperties().Select(c => c.ToString())).ToList();
            for (int i = 0; i < colors.Count(); i++)
            {
                colors[i] = colors[i].Substring(17);
            }
            return colors;
        }

        private static IEnumerable<string> GetBlurhashs()
        {
            var blurHash = new List<string>
                {
                    "LEHV6nWB2yk8pyo0adR*.7kCMdnj",
                    "LFC$yHwc8^$yIAS$%M%00KxukYIp",
                    "LoC%a7IoIVxZ_NM|M{s:%hRjWAo0",
                    "LFC$yHwc8^$yIAS$%M%00KxukYIp",
                };
            return blurHash;
        }
    }
}
