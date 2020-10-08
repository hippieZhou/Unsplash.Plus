using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsplash.Plus.Models
{
    public class Collection
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsCurated { get; set; }
        public bool IsFeatured { get; set; }
        public int TotalPhotos { get; set; }
        public bool IsPrivate { get; set; }
        public string ShareKey { get; set; }
        public Photo CoverPhoto { get; set; }
        public User User { get; set; }
        public CollectionLinks Links { get; set; }
    }
}
