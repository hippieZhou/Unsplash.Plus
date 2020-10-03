using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Plus.Helpers;
using Unsplash.Plus.Models;
using Unsplasharp;
using Unsplasharp.Models;

namespace Unsplash.Plus.Services
{
    /// <summary>
    /// https://github.com/rootasjey/unsplasharp
    /// https://unsplash.dogedoge.com/?hao.su
    /// </summary>
    public interface IUnsplashService
    {
        Task<IEnumerable<PhotoItem>> GetDesignPhotoList(int pageIndex, int pageSize);
        Task<IEnumerable<PhotoItem>> ListPhotos(int count, int pageSize);
        Task ListCollections();
    }

    public class UnsplashService: IUnsplashService
    {
        public  const string ApplicationId = "b27797f08021cb0d84672e38530613898facf6d4b06c62ec7dd5a2912c9a4438";
        public const string Secret = "38b6f87419f686094e53286d0b6cb39efc3fc97ee6ce3bb6e0b90531aec9984f";

        private readonly UnsplasharpClient _client;
        private readonly IMapper _mapper;

        public UnsplashService(IConfigurationRoot configurationRoot, IMapper mapper)
        {
            //var accessKey = configurationRoot.GetSection("unsplash:AccessKey").Value;
            //var secretKey = configurationRoot.GetSection("unsplash:SecretKey").Value;
            //_client = new UnsplasharpClient(accessKey, secretKey);
            _client = new UnsplasharpClient(ApplicationId, Secret);

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<PhotoItem>> GetDesignPhotoList(int pageIndex, int pageSize)
        {
            var items = ColorBrushHelper.Colors.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PhotoItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Color = "#60544D",
                    Urls = new PhotoUrls
                    {
                        Small = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                        Full = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                        Regular = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                        Thumbnail = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                        Custom = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                    }
                });
            return Task.FromResult(items);
        }

        public async Task<IEnumerable<PhotoItem>> ListPhotos(int page, int pageSize)
        {
            var photos = await _client.ListPhotos(page, pageSize);
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoItem>>(photos);

        }

        public async Task ListCollections()
        {
            List<Collection> collections =  await _client.ListCollections();
        }
    }
}
