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
    public class DetailsModel : PageModel
    {
        private readonly TMS.NET06.Eos.Razor.Data.EosContext _context;

        public DetailsModel(TMS.NET06.Eos.Razor.Data.EosContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
