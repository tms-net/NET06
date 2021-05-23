using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Parfume.Manager.MVC.Models
{
    public class ProductDetailsViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Volume { get; set; }
        public string  PageUrl { get; set; }
        public IList<string> ImageUrls { get; set; }

        public IList<string> ImageCarousel { get; set; }

        public IList<string> ParentsNodeUrls { get; set; }

        public string Overview { get; set; }

        public string ReviewUrl { get; set; }

        public int Rating { get; set; }

        public bool Avaibility { get; set; }

        public ProductDetailsViewModel()
        {
            ImageUrls = new List<string>();
            ParentsNodeUrls = new List<string>();
            ImageCarousel = new List<string>();
            
        }
    }
}
