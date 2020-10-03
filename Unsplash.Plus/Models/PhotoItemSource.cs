using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unsplash.Plus.Services;

namespace Unsplash.Plus.Models
{
    public class PhotoItemSource : IIncrementalSource<PhotoItem>
    {
        private readonly IUnsplashService unsplashService;

        public PhotoItemSource(IUnsplashService unsplashService, bool loadInMemory = true)
        {
            this.unsplashService = unsplashService ?? throw new ArgumentNullException(nameof(unsplashService));
            ImageCache.Instance.CacheDuration = TimeSpan.FromHours(24);
            ImageCache.Instance.MaxMemoryCacheCount = loadInMemory ? 200 : 0;
        }

        public async Task<IEnumerable<PhotoItem>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var items = await unsplashService.ListPhotos(pageIndex, pageSize);
            //var items = await unsplashService.GetDesignPhotoList(pageIndex, pageSize);
            return items;
        }
    }
}
