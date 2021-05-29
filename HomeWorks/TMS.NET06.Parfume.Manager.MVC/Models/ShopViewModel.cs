using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.Parfume.Manager.MVC.Data.Models;

namespace TMS.NET06.Parfume.Manager.MVC.Models
{
    public class ShopViewModel
    {
        public ShopViewModel()
        {
            Products = new List<ShortProductViewModel>();
            Brands = new List<MenuBrandViewModel>();
        }

        // public IList<MenuSelectViewModel> MenuSelect { get; set; }

        public IList<MenuBrandViewModel> Brands { get; set; }

        public IList<ShortProductViewModel> Products { get; set; }

    }
    public class ShortProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }

        public string ProductDetailsUrl { get; set; }
        public string ImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
    }

    //public class MenuSelectViewModel
    //{
    //    public IList<MenuBrandViewModel> SelectedBrands { get; set; }
    //    public MenuGenderViewModel menuGenderViewModel { get; set; }
      
    //}

    public class MenuBrandViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

    }

   
}

