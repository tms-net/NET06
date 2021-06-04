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
            SelectedQuantityOnPage = 12;
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
    }
}
