using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Parfume.Manager.MVC.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            HomeBlocks = new List<HomeBlockViewModel>();
        }

        public IList<HomeBlockViewModel> HomeBlocks { get; set; }
    }

    public class HomeBlockViewModel
    {
        public decimal PriceFrom { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string PageUrl { get; set; }
    }
}
