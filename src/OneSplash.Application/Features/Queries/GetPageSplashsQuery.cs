using AutoMapper;
using MediatR;
using OneSplash.Application.Design;
using OneSplash.Application.DTOs;
using OneSplash.Application.Wrappers;
using OneSplash.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OneSplash.Application.Features.Queries
{
    public class GetPagedSplashsQuery : IRequest<PagedResponse<IEnumerable<SplashPhotoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetPagedSplashsQueryHandler : IRequestHandler<GetPagedSplashsQuery, PagedResponse<IEnumerable<SplashPhotoDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IUnSplashPhotoService _splashService;

            public GetPagedSplashsQueryHandler(
                IMapper mapper,
                IUnSplashPhotoService splashService)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(splashService));
                _splashService = splashService ?? throw new ArgumentNullException(nameof(splashService));

            }
            public async Task<PagedResponse<IEnumerable<SplashPhotoDto>>> Handle(GetPagedSplashsQuery request, CancellationToken cancellationToken)
            {
                //var entities = await _splashService.ListPhotos(request.PageNumber, request.PageSize);
                //var dtos = _mapper.Map<IEnumerable<SplashPhotoEntity>, IEnumerable<SplashPhotoDto>>(entities);
                //return new PagedResponse<IEnumerable<SplashPhotoDto>>(dtos, request.PageNumber, request.PageSize);

                await Task.Delay(500);
                var items = await DesignDataFaker.GenerateSplashPhotosAsync(request.PageSize);
                return new PagedResponse<IEnumerable<SplashPhotoDto>>(items, request.PageNumber, request.PageSize);
            }
        }
    }
}
