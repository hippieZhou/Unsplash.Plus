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
        Task<IEnumerable<PhotoItem>> GetRandomPhotoList(int count);
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
                .Select(x => new PhotoItem { Color = x.Key });
            return Task.FromResult(items);
        }

        public async Task<IEnumerable<PhotoItem>> GetRandomPhotoList(int count)
        {
            var photos = await _client.GetRandomPhoto(count);
            var items = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoItem>>(photos);
            return items;
        }
    }
}
