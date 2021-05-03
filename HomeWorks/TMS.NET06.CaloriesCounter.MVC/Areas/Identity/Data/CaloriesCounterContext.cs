using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.NET06.CaloriesCounter.MVC.Areas.Identity.Data;

namespace TMS.NET06.CaloriesCounter.MVC.Data
{
    public class CaloriesCounterContext : IdentityDbContext<CaloriesCounterUser>
    {
        public CaloriesCounterContext(DbContextOptions<CaloriesCounterContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
