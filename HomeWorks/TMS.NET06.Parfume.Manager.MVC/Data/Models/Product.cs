using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Parfume.Manager.MVC.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public Gender? Gender { get; set; }

        public int Volume { get; set; }
        public string[] Images { get; set; }

        public string[] ImagesSmall { get; set; }
    }

    public enum Gender 
    {
        men = 1,
        women
    }
}
