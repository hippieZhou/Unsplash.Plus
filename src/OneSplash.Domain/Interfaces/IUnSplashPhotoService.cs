using OneSplash.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneSplash.Domain.Interfaces
{
    public interface IUnSplashPhotoService
    {
        Task<IEnumerable<SplashPhotoEntity>> ListPhotos(int page, int pageSize);
        Task ListCollections(int page, int pageSize);
    }
}
