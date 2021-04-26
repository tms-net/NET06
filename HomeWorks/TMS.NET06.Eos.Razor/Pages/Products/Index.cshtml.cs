using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMS.NET06.Eos.Razor.Data;
using TMS.NET06.Eos.Razor.Models;

namespace TMS.NET06.Eos.Razor
{
    public class IndexModel : PageModel
    {
        private readonly TMS.NET06.Eos.Razor.Data.EosContext _context;

        public IndexModel(TMS.NET06.Eos.Razor.Data.EosContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
