using MediatR;
using OneSplash.Application.DTOs;
using OneSplash.Application.Parameters;
using OneSplash.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;

namespace OneSplash.Application.Features.Queries
{
    public class GetPagedSplashsParameter : RequestParameter
    {
    }

    public class GetPagedSplashsQuery : IRequest<PagedResponse<IEnumerable<SplashDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetAllProductsQueryHandler : IRequestHandler<GetPagedSplashsQuery, PagedResponse<IEnumerable<SplashDto>>>
        {
            public async Task<PagedResponse<IEnumerable<SplashDto>>> Handle(GetPagedSplashsQuery request, CancellationToken cancellationToken)
            {
                var items = GetRecipeList(request.PageSize);
                await Task.Delay(500);
                return new PagedResponse<IEnumerable<SplashDto>>(items, request.PageNumber, request.PageSize);
            }


            public static IEnumerable<SplashDto> GetRecipeList(int count = 1000)
            {
                // Initialize list of recipes for varied image size layout sample
                var rnd = new Random();
                List<SplashDto> tempList = new List<SplashDto>(
                                            Enumerable.Range(0, count).Select(k =>
                                                new SplashDto
                                                {
                                                    ImageAuthor = "Recipe " + k.ToString(),
                                                    Color = GetColors().ElementAt((k % 100) + 1),
                                                    ImageUri = GetImageUri().ElementAt(rnd.Next(0, 9)),
                                                    Blurhash = GetBlurhashs().ElementAt(rnd.Next(0, 4))
                                                }));
                return tempList;
            }

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

            private static IEnumerable<string> GetImageUri()
            {
                return new List<string>
            {
                "/Assets/Images/bantersnaps-wPMvPMD9KBI-unsplash.jpg",
                "/Assets/Images/eva-dang-EXdXLrZXS9Q-unsplash.jpg",
                "/Assets/Images/tomas-nozina-UP22zkjJGZo-unsplash.jpg",
                "/Assets/Images/ashim-d-silva-WeYamle9fDM-unsplash.jpg",
                "/Assets/Images/annie-spratt-tB4Gf7ddcJY-unsplash.jpg",
                "/Assets/Images/damian-patkowski-QeC4oPdKu7c-unsplash.jpg",
                 "/Assets/Images/willian-west-YpKiwlvhOpI-unsplash.jpg",
                 "/Assets/Images/felix-NAytNmKtyiU-unsplash.jpg",
                 "/Assets/Images/willian-west-TVyjcTEKHLU-unsplash.jpg"
            };
            }
        }
    }
}
