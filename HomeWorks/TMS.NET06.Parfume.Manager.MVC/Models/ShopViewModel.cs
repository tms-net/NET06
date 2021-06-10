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

            SortValues.Add(1, "Price ascending");
            SortValues.Add(2, "Price descending");
            SortValues.Add(3, "Popular first");

            SelectedPage = 1;
            SelectedSort = 1;

        }

        // public IList<MenuSelectViewModel> MenuSelect { get; set; }

        public IList<MenuBrandViewModel> Brands { get; set; }

        public IList<ShortProductViewModel> Products { get; set; }

        public Gender? SelectedGender { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public int[] QuantityOnPage = new int[] { 4, 24, 48, 96 };

        public int SelectedQuantityOnPage { get; set; }

        public Dictionary<int, string> SortValues = new Dictionary<int, string>(5);

        public int SelectedSort { get; set; }

        public int QuantityOfPages { get; set; }

        public int SelectedPage { get; set; }

        public int TotalProductsCount { get; set; }

    }
    public class ShortProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
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

