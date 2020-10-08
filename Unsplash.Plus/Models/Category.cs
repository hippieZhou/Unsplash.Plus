using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsplash.Plus.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int PhotoCount { get; set; }
        public CategoryLinks Links { get; set; }
    }
}
