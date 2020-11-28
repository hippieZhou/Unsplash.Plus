using MediatR;
using OneSplash.Application.Design;
using OneSplash.Application.DTOs;
using OneSplash.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace OneSplash.Application.Features.Queries
{
    public class GetTodaySplashQuery : IRequest<Response<SplashPhotoDto>>
    {
        public class GetTodaySplashQueryHandler : IRequestHandler<GetTodaySplashQuery, Response<SplashPhotoDto>>
        {
            public async Task<Response<SplashPhotoDto>> Handle(GetTodaySplashQuery request, CancellationToken cancellationToken)
            {
                var item = await DesignDataFaker.GenerateSplashPhotoDtoAsync();
                return new Response<SplashPhotoDto>(item);
            }
        }
    }
}
