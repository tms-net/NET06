using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Eos.Razor.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        [NotMapped]
        public DateTime? CreationDate { get; set; }
    }
}
