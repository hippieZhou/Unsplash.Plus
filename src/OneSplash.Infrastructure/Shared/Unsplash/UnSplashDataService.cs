using Microsoft.Extensions.Options;
using OneSplash.Domain.Entities;
using OneSplash.Domain.Interfaces;
using OneSplash.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unsplasharp;

namespace OneSplash.Infrastructure.Shared.Unsplash
{
    public class UnSplashDataService : ISplashService
    {
        public const string ApplicationId = "b27797f08021cb0d84672e38530613898facf6d4b06c62ec7dd5a2912c9a4438";
        public const string Secret = "38b6f87419f686094e53286d0b6cb39efc3fc97ee6ce3bb6e0b90531aec9984f";

        private readonly UnsplasharpClient _client;

        public UnSplashDataService(IOptions<AppSettings> options)
        {
            var accessKey = options.Value.AccessKey ?? ApplicationId;
            var secretKey = options.Value.Secret ?? Secret;
            _client = new UnsplasharpClient(accessKey, secretKey);
        }

        public async Task<IEnumerable<SplashPhotoEntity>> ListPhotos(int page, int pageSize)
        {
            var items = await _client.ListPhotos(page, pageSize);
            var entities = new List<SplashPhotoEntity>();
            items.ForEach(item => 
            {
                entities.Add(new SplashPhotoEntity
                {
                    Blurhash = item.BlurHash,
                    ImageAuthor = item.User?.Username,
                    Color = item.Color,
                    ImageUri = item.Urls.Small,
                    Width = item.Width,
                    Height = item.Height
                });
            });
            return entities;
        }
        public Task ListCollections(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
