using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.Parfume.MVC.Kovaleva.Data;
using TMS.NET06.Parfume.MVC.Kovaleva.Models;

namespace TMS.NET06.Parfume.MVC.Kovaleva.Controllers
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

        public async Task<IActionResult> Index()
        {
            return View(await db.Brand.ToListAsync());
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
