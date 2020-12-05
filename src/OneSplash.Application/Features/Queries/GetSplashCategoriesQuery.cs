using MediatR;
using OneSplash.Application.DTOs;
using OneSplash.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;

namespace OneSplash.Application.Features.Queries
{
    public class GetSplashCategoriesQuery : IRequest<Response<IEnumerable<CategoryDto>>>
    {
        public class GetSplashCategoriesQueryHandler : IRequestHandler<GetSplashCategoriesQuery, Response<IEnumerable<CategoryDto>>>
        {
            public Task<Response<IEnumerable<CategoryDto>>> Handle(GetSplashCategoriesQuery request, CancellationToken cancellationToken)
            {
                var colors = GetColors();
                var items = from color in colors select new CategoryDto { Name = color };
                return Task.FromResult(new Response<IEnumerable<CategoryDto>>(items));
            }

            private IList<string> GetColors()
            {
                IList<string> colors = (typeof(Colors).GetRuntimeProperties().Select(c => c.ToString())).ToList();
                for (int i = 0; i < colors.Count(); i++)
                {
                    colors[i] = colors[i].Substring(17);

                }
                return colors;
            }
        }
    }
}
