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
    }

    public class UnsplashService: IUnsplashService
    {
        private readonly UnsplasharpClient _client;
        private readonly IMapper _mapper;

        public UnsplashService(IConfigurationRoot configurationRoot, IMapper mapper)
        {
            var AccessKey = configurationRoot.GetSection("unsplash:AccessKey").Value;
            var SecretKey = configurationRoot.GetSection("unsplash:SecretKey").Value;
            _client = new UnsplasharpClient(AccessKey, SecretKey);
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
                    Small = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                    Full = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                    Regular = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                    Thumbnail = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                    Raw = "https://cn.bing.com/th?id=OHR.LaragangaMoth_ZH-CN2013788793_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=HpEdgeAn",
                });
            return Task.FromResult(items);
        }

        public async Task<IEnumerable<PhotoItem>> ListPhotos(int page, int pageSize)
        {
            var photos = await _client.ListPhotos(page, pageSize);
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoItem>>(photos);
        }
    }
}
