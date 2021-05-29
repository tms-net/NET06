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
        [BindProperty(Name = "brand", SupportsGet = true)]
        public IList<string> SelectedBrands { get; set; }

        [BindProperty(Name = "gender", SupportsGet = true)]
        public Gender? SelectedGender { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }
    }
}
