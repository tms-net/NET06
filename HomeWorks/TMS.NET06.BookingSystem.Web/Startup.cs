using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBookingRepository, EFBookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => {
                try {
                    await next();
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync(ex.ToString());
                }
                finally
                {
                    await context.Response.WriteAsync("Hello User!\n");
                }
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello From Middleware!");
                }); 
                
                endpoints.MapGet("/cs", async context =>
                {
                    var config = context.RequestServices.GetService<IConfiguration>();
                    await context.Response.WriteAsync(config.GetConnectionString("BookingDb"));
                });

                endpoints.MapGet("/services", async context =>
                {
                    var repo = context.RequestServices.GetService<IBookingRepository>();
                    await context.Response.WriteAsync(
                            string.Join("\n", (await repo.GetServicesAsync()).Select(s => s.Name))
                        );
                });

                endpoints.MapGet("/addservice", async context =>
                {
                    if (context.Request.Query.ContainsKey("name"))
                    {
                        var repo = context.RequestServices.GetService<IBookingRepository>();
                        var service = new Service { Name = context.Request.Query["name"]};
                        var serviceId = await repo.AddServiceAsync(service);

                        await context.Response.WriteAsync($"Added service {service.Name} with ID: {serviceId}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Nothing to create");
                    }
                });
            });
        }
    }
}
