using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.Parfume.Manager.MVC.Data.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Uri(UriKind.Relative)]
        public string Logo { get; set; }
    }

    public class UriAttribute : Attribute
    {
        private UriKind _uriKind { get; }

        public UriAttribute(UriKind uriKind)
        {
            _uriKind = uriKind;
        }

        public bool IsValid(string str) => Uri.IsWellFormedUriString(str, _uriKind);
    }
}
