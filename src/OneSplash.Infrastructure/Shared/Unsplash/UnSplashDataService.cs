using OneSplash.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace OneSplash.Infrastructure.Shared.Unsplash
{
    public class UnSplashDataService : ISplashService
    {
        public Task ListCollections(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task ListPhotos(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
