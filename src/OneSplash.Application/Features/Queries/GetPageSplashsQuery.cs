﻿using AutoMapper;
using MediatR;
using OneSplash.Application.DTOs;
using OneSplash.Application.Parameters;
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
    public class GetPagedSplashsParameter : RequestParameter
    {
    }

    public class GetPagedSplashsQuery : IRequest<PagedResponse<IEnumerable<SplashPhotoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetAllProductsQueryHandler : IRequestHandler<GetPagedSplashsQuery, PagedResponse<IEnumerable<SplashPhotoDto>>>
        {
            private readonly IMapper _mapper;
            private readonly ISplashService _splashService;

            public GetAllProductsQueryHandler(
                IMapper mapper,
                ISplashService splashService)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(splashService));
                _splashService = splashService ?? throw new ArgumentNullException(nameof(splashService));
            }
            public async Task<PagedResponse<IEnumerable<SplashPhotoDto>>> Handle(GetPagedSplashsQuery request, CancellationToken cancellationToken)
            {
                var entities = await _splashService.ListPhotos(request.PageNumber, request.PageSize);
                var dtos = _mapper.Map<IEnumerable<SplashPhotoEntity>, IEnumerable<SplashPhotoDto>>(entities);
                return new PagedResponse<IEnumerable<SplashPhotoDto>>(dtos, request.PageNumber, request.PageSize);

                //var items = GetRecipeList(request.PageSize);
                //await Task.Delay(500);
                //return new PagedResponse<IEnumerable<SplashPhotoDto>>(items, request.PageNumber, request.PageSize);
            }


            public static IEnumerable<SplashPhotoDto> GetRecipeList(int count = 1000)
            {
                // Initialize list of recipes for varied image size layout sample
                var rnd = new Random();
                List<SplashPhotoDto> tempList = new List<SplashPhotoDto>(
                                            Enumerable.Range(0, count).Select(k =>
                                                new SplashPhotoDto
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
