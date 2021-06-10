using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.Parfume.Manager.MVC.Data.Models;

namespace TMS.NET06.Parfume.Manager.MVC.Models
{
    public class ShopViewRequest
    {
        public ShopViewRequest()
        {
            PriceMin = 0;
            PriceMax = 200;
            SelectedQuantityOnPage = 4;
            SelectedSort = 1;
            SelectedPage = 1;
        }

        [BindProperty(Name = "brand", SupportsGet = true)]
        public IList<string> SelectedBrands { get; set; }

        [BindProperty(Name = "gender", SupportsGet = true)]
        public Gender? SelectedGender { get; set; }

        [BindProperty(Name = "pricemin", SupportsGet = true)]
        public decimal? PriceMin { get; set; }

        [BindProperty(Name = "pricemax", SupportsGet = true)]
        public decimal? PriceMax { get; set; }

        [BindProperty(Name = "selectedquantity", SupportsGet = true)]
        public int SelectedQuantityOnPage { get; set; }

        [BindProperty(Name = "selectedsort", SupportsGet = true)]
        public int SelectedSort { get; set; }

        [BindProperty(Name = "page", SupportsGet = true)]
        public int SelectedPage { get; set; }
    }
}
