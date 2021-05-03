using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.NET06.CaloriesCounter.MVC.Areas.Identity.Data;
using TMS.NET06.CaloriesCounter.MVC.Data;

[assembly: HostingStartup(typeof(TMS.NET06.CaloriesCounter.MVC.Areas.Identity.IdentityHostingStartup))]
namespace TMS.NET06.CaloriesCounter.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CaloriesCounterContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CaloriesCounterContextConnection")));

                services.AddDefaultIdentity<CaloriesCounterUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CaloriesCounterContext>();
            });
        }
    }
}