using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsplash.Plus.Models
{
    public class User
    {
        public int Downloads { get; set; }
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public bool FollowedByUser { get; set; }
        public string UpdatedAt { get; set; }
        public int TotalCollections { get; set; }
        public int TotalPhotos { get; set; }
        public int TotalLikes { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string PortfolioUrl { get; set; }
        public string TwitterUsername { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
        public ProfileImage Profiles { get; set; }
    }
}
