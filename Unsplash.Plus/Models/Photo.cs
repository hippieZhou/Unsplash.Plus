﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Unsplash.Plus.Models
{
    public class Photo
    {
        public Location Location { get; set; }
        public Exif Exif { get; set; }
        public User User { get; set; }
        public List<Category> Categories { get; set; }
        public Urls Urls { get; set; }
        public List<Collection> CurrentUserCollection { get; set; }
        public bool IsLikedByUser { get; set; }
        public int Likes { get; set; }
        public int Downloads { get; set; }
        public string BlurHash { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedAt { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public PhotoLinks Links { get; set; }
    }
}
