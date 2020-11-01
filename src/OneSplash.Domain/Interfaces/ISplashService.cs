using System.Threading.Tasks;

namespace OneSplash.Domain.Interfaces
{
    public interface ISplashService
    {
        Task ListPhotos(int page, int pageSize);
        Task ListCollections(int page, int pageSize);
    }
}
