using AutoMapper;
using Bogus;
using MediatR;
using OneSplash.Application.DTOs;
using OneSplash.Application.Wrappers;
using OneSplash.Domain.Entities;
using OneSplash.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;

namespace OneSplash.Application.Features.Queries
{
    public class GetPagedSplashsQuery : IRequest<PagedResponse<IEnumerable<SplashPhotoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetAllProductsQueryHandler : IRequestHandler<GetPagedSplashsQuery, PagedResponse<IEnumerable<SplashPhotoDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IUnSplashPhotoService _splashService;
            private readonly Faker<SplashPhotoDto> _testOrders;

            public GetAllProductsQueryHandler(
                IMapper mapper,
                IUnSplashPhotoService splashService)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(splashService));
                _splashService = splashService ?? throw new ArgumentNullException(nameof(splashService));
                _testOrders = new Faker<SplashPhotoDto>()
                    .StrictMode(true)
                    .RuleFor(x => x.ImageAuthor, f => f.Internet.UserName())
                    .RuleFor(x => x.Color, f => f.PickRandom(GetColors()))
                    .RuleFor(x => x.ImageUri, f => f.Image.PicsumUrl())
                    .RuleFor(x => x.Width, f => f.Random.Int())
                    .RuleFor(x => x.Height, f => f.Random.Int())
                    .RuleFor(x => x.Blurhash, f => f.PickRandom(GetBlurhashs()));
            }
            public async Task<PagedResponse<IEnumerable<SplashPhotoDto>>> Handle(GetPagedSplashsQuery request, CancellationToken cancellationToken)
            {
                //var entities = await _splashService.ListPhotos(request.PageNumber, request.PageSize);
                //var dtos = _mapper.Map<IEnumerable<SplashPhotoEntity>, IEnumerable<SplashPhotoDto>>(entities);
                //return new PagedResponse<IEnumerable<SplashPhotoDto>>(dtos, request.PageNumber, request.PageSize);

                var items = _testOrders.Generate(request.PageSize);
                await Task.Delay(500);
                return new PagedResponse<IEnumerable<SplashPhotoDto>>(items, request.PageNumber, request.PageSize);
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
        }
    }
}
