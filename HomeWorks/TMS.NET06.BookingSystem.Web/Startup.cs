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
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.Use(async (context, next) => {
                await next();
            });

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
                    await context.Response.WriteAsync($"Information for GetServicesAsync. \n");

                    var repo = context.RequestServices.GetService<IBookingRepository>();
                    await context.Response.WriteAsync(
                            string.Join("\n", (await repo.GetServicesAsync()).Select(s => s.Name))
                        );
                });

                endpoints.MapGet("/services/{serviceId:int}", async context =>
                {
                    var serviceId = context.Request.RouteValues["serviceId"];
                    var repo = context.RequestServices.GetService<IBookingRepository>();
                    var service = await repo.GetServiceAsync(int.Parse(serviceId.ToString()));
                    if (service == null)
                        context.Response.StatusCode = 404;
                    else
                        await context.Response.WriteAsync(service.Name);
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

                endpoints.MapGet("/clients", async context =>
                {
                    await context.Response.WriteAsync($"Information for GetClientsAsync. \n");

                    var repo = context.RequestServices.GetService<IBookingRepository>();
                    await context.Response.WriteAsync(
                        string.Join("\n", (await repo.GetClientsAsync()).Select(s => s.Name))
                    );
                });

                endpoints.MapGet("/addclient", async context =>
                {
                    if (context.Request.Query.ContainsKey("name"))
                    {
                        var repo = context.RequestServices.GetService<IBookingRepository>();
                        var client = new Client() { Name = context.Request.Query["name"] };
                        var clientId = await repo.AddClientAsync(client);

                        await context.Response.WriteAsync($"Added service {client.Name} with ID: {clientId}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Nothing to create client");
                    }
                });
                
                endpoints.MapGet("/bookings", async context =>
                {
                    var resStart = DateTime.TryParse(context.Request.Query["start"], out var startPeriod);

                    var resEnd = DateTime.TryParse(context.Request.Query["end"], out var endPeriod);

                    await context.Response.WriteAsync($"Information for GetBookingEntriesAsync. \n");

                    if (resStart && resEnd)
                    {
                        var repo = context.RequestServices.GetService<IBookingRepository>();
                        await context.Response.WriteAsync(
                            string.Join("\n", (await repo.GetBookingEntriesAsync(startPeriod, endPeriod, BookingStatus.Confirmed)).Select(s => s.Client.Name))
                        );
                    }
                    else
                    {
                        var repo = context.RequestServices.GetService<IBookingRepository>();
                        await context.Response.WriteAsync(
                            string.Join("\n", (await repo.GetBookingEntriesAsync(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(20), BookingStatus.Confirmed)).Select(s => s.Client.Name))
                        );
                    }
                });

                endpoints.MapGet("/addbookings", async context =>
                {
                    //var resClientId = Int32.TryParse(context.Request.Query["clientid"], out var clientId);
                    
                    if (int.TryParse(context.Request.Query["clientid"], out var clientId))
                    {
                        var repo = context.RequestServices.GetService<IBookingRepository>();
                        await context.Response.WriteAsync(
                            string.Join("\n", (await repo.GetClientBookingsAsync(clientId)).Select(s => s.Client))
                        );
                        await context.Response.WriteAsync($"Information for clientId = {clientId}.\n");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Nothing information to view.");
                    }
                });

                endpoints.MapGet("/savebooking", async context =>
                {

                    if (int.TryParse(context.Request.Query["bookingid"], out var bookingid))
                    {
                        var comm = context.Request.Query["comment"];

                        var repo = context.RequestServices.GetService<IBookingRepository>();

                        var oldBooking = await repo.GetBookingAsync(bookingid);

                        oldBooking.Comment = comm;

                        if (await repo.SaveEntryAsync(oldBooking))
                            await context.Response.WriteAsync($"Information update for bookingid = {bookingid}.\n");
                        else await context.Response.WriteAsync($"Information don`t update for bookingid = {bookingid}.\n");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Nothing information to save.");
                    }
                });
            });


            var repo = app.ApplicationServices.GetService<IBookingRepository>();
            await repo.InitAsync();
        }
    }
}
