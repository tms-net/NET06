using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.CaloriesCounter.MVC.Models
{
    public class ProductRow
    {
        [DisplayName("Название")]
        public string Name { get; set; }

        public string Mass { get; set; }

        public string Calories { get; set; }
    }
}
