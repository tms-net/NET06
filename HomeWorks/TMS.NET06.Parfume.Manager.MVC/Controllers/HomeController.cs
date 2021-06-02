using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.Parfume.Manager.MVC.Data;
using TMS.NET06.Parfume.Manager.MVC.Data.Models;
using TMS.NET06.Parfume.Manager.MVC.Models;


namespace TMS.NET06.Parfume.Manager.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ParfumeShopContext db;

        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env, ILogger<HomeController> logger, ParfumeShopContext context)
        {
            _logger = logger;
            db = context;
            _env = env;
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

        public IActionResult ProductDetails()
        {
          
            var productViewModel = new ProductDetailsViewModel();

            productViewModel.Name = "Chanel #5";
            productViewModel.Price = 55;
            productViewModel.Volume = 50;
            productViewModel.PageUrl = "/";
            productViewModel.ImageUrls.Add("~/img/product-img/pro-big-1.jpg");
            productViewModel.ImageUrls.Add("~/img/product-img/pro-big-2.jpg");
            productViewModel.ImageUrls.Add("~/img/product-img/pro-big-3.jpg");
            productViewModel.ImageUrls.Add("~/img/product-img/pro-big-4.jpg");

            foreach (var imageUrl in productViewModel.ImageUrls)
            {
                string imageStr = imageUrl.Replace("~", "");
                productViewModel.ImageCarousel.Add(imageStr);
            }

            productViewModel.ParentsNodeUrls.Add("Women");
            productViewModel.ParentsNodeUrls.Add("Chanel");
           // productViewModel.RefPath.Add("Chanel #5");

            productViewModel.Overview = "Духи Chanel № 5 заслуженно носят звание лучших в мире. Они проверены временем, но не подвластны ему. Его называют таинственным, роскошным. Его ощущение на себе повышает настроение, он нравится и мужчинам. Большинство людей говорит о нем, как о приятном, но слегка терпком аромате, который слышно на протяжении 3 – 6 часов, что зависит и от погоды, от того из какого материала одежда и личного восприятия человека. Однозначно то, что они пахнут женщиной, о чем говорила и Коко Шанель.";
            productViewModel.Rating = 5;
            productViewModel.Avaibility = true;
            productViewModel.ReviewUrl = "/";

            return View(productViewModel);
        }

    public IActionResult Shop(ShopViewRequest request)
        {
            var shopViewModel = new ShopViewModel();

            var brands = db.Brands.ToList();

            foreach (var brand in brands)
            {
                shopViewModel.Brands.Add(new MenuBrandViewModel { 
                    Name = brand.Name,
                    Id = brand.BrandId.ToString(),
                    IsChecked = request.SelectedBrands != null && request.SelectedBrands.Contains(brand.BrandId.ToString())
                }) ;
            }


            string webRootPath = _env.WebRootPath;

            string imagePath = "~/img/product-img/product";

            string imagePath1 = "/img/product-img/product";

            var path = Path.Combine(_env.WebRootPath, imagePath1);

            

            //string p = HttpContext.Current.Server.MapPath("/UploadedFiles");

           
            List <Product> products  = null;
            //if (brandID != null) { 
            if (request.SelectedBrands != null && request.SelectedGender != null)
                products = db.Products.Where(p => (request.SelectedBrands.Contains(p.BrandId.ToString()) && p.Gender == request.SelectedGender)).ToList();
            else if (request.SelectedBrands != null && request.SelectedGender == null)
                products = db.Products.Where(p => request.SelectedBrands.Contains(p.BrandId.ToString())).ToList();
            else if (request.SelectedBrands == null && request.SelectedGender != null)
                products = db.Products.Where(p => (p.Gender == request.SelectedGender)).ToList();
            else
                products = db.Products.ToList();

            foreach (var product in products)
            {
                var shortProductViewModel = new ShortProductViewModel();

                shortProductViewModel.Name = product.Name;
                shortProductViewModel.Price = product.Price;

                shortProductViewModel.ImageUrl = imagePath + product.ImageId.ToString() + ".jpg";


                string hoverImageUrl = imagePath + product.ImageId.ToString() + "_h.jpg";
                if (System.IO.File.Exists(hoverImageUrl))
                    shortProductViewModel.HoverImageUrl = hoverImageUrl;
                else
                    shortProductViewModel.HoverImageUrl = shortProductViewModel.ImageUrl;

                shortProductViewModel.Rating = 5;
                shortProductViewModel.ProductDetailsUrl = "/";

                shopViewModel.Products.Add(shortProductViewModel);

                

            }

            //  shopViewModel.SelectedGender = request.SelectedGender!=null ? request.SelectedGender : Gender.men;
            shopViewModel.SelectedGender = request.SelectedGender;
            return View(shopViewModel);

            //}

            ////====================
            //var shortProductViewModel = new ShortProductViewModel();

            //shortProductViewModel.Name = "Chanel #5";
            //shortProductViewModel.Price = 55;

            //shortProductViewModel.ImageUrl = "~/img/product-img/product1.jpg";
            //shortProductViewModel.HoverImageUrl = "~/img/product-img/product2.jpg";
            //shortProductViewModel.Rating = 5;
            //shortProductViewModel.ProductDetailsUrl = "/";

            //shopViewModel.Products.Add(shortProductViewModel);
            //return View(shopViewModel);
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
