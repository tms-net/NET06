using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.Parfume.Manager.MVC.Data;
using TMS.NET06.Parfume.Manager.MVC.Models;

namespace TMS.NET06.Parfume.Manager.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ParfumeShopContext db;
        public HomeController(ILogger<HomeController> logger, ParfumeShopContext context)
        {
            _logger = logger;
            db = context;
        }

        //public IActionResult Index()
        //{
        //    return View(db.Brand.ToList());
        //}

        public  IActionResult Index()
        {
            //return View(await db.Brand.ToListAsync());
            var homeViewModel = new HomeViewModel();
            var homeBlockViewModel = new HomeBlockViewModel();
            homeBlockViewModel.Title = "Chanel";
            homeBlockViewModel.PriceFrom = 180;
            homeBlockViewModel.ImageUrl = "~/img/bg-img/1.jpg";
            homeBlockViewModel.PageUrl = "/";
            homeViewModel.HomeBlocks.Add(homeBlockViewModel);
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
