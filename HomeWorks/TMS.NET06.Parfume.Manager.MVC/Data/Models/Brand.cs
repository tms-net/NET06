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

       // [Uri(UriKind.Relative)]
        public string Logo { get; set; }

        public string FrontImage { get; set; }

        public IList<Product> Products; //mine

        public Brand()
        {
            Products = new List<Product>();
        }

        // private ICollection<Product> _products;
        //public ICollection<Product> Products
        //{
        //    get => LazyLoader.Load(this, ref _products);
        //    set => _products = value;
        //} // Collection Navigation Property

        //private Brand(Action<object, string> lazyLoader)
        //{
        //    LazyLoader = lazyLoader;
        //}

        //private Action<object, string> LazyLoader { get; }
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
