using Microsoft.EntityFrameworkCore;
using TMS.NET06.Eos.Razor.Models;

namespace TMS.NET06.Eos.Razor.Data
{
    public class EosContext : DbContext
    {
        public EosContext (DbContextOptions<EosContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
