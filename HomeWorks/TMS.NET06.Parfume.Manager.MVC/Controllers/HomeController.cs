using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public IActionResult Index()
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

        public IActionResult ProductDetails(int id)
        {
            Product product = db.Products.Where(p => p.ProductId == id).First();
            product.Brand = db.Brands.Where(b => b.BrandId == product.BrandId).First();

            var productViewModel = new ProductDetailsViewModel();

            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.Volume = product.Volume;
            productViewModel.PageUrl = "/";

            string directoryPath = Path.Combine(_env.WebRootPath, "img", "prod-img", id.ToString());
            string imagePathRel = String.Concat("~/img/prod-img/", id.ToString());

            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);
               
                foreach (string s in files)
                {
                    string fileNameShort = s.Replace(directoryPath + "\\", "");
                   productViewModel.ImageUrls.Add(imagePathRel + "/" + fileNameShort);
                }
            }

            //productViewModel.ImageUrls.Add("~/img/product-img/pro-big-1.jpg");
            //productViewModel.ImageUrls.Add("~/img/product-img/pro-big-2.jpg");
            //productViewModel.ImageUrls.Add("~/img/product-img/pro-big-3.jpg");
            //productViewModel.ImageUrls.Add("~/img/product-img/pro-big-4.jpg");

            foreach (var imageUrl in productViewModel.ImageUrls)
            {
                string imageStr = imageUrl.Replace("~", "");
                productViewModel.ImageCarousel.Add(imageStr);
            }

            productViewModel.ParentsNodeUrls.Add(product.Gender.ToString());
            productViewModel.ParentsNodeUrls.Add(product.Brand.Name);
            // productViewModel.RefPath.Add("Chanel #5");

            productViewModel.Overview = "Духи " + product.Name + " заслуженно носят звание лучших в мире. Они проверены временем, но не подвластны ему. Его называют таинственным, роскошным. Его ощущение на себе повышает настроение, он нравится и мужчинам. Большинство людей говорит о нем, как о приятном, но слегка терпком аромате, который слышно на протяжении 3 – 6 часов, что зависит и от погоды, от того из какого материала одежда и личного восприятия человека. Однозначно то, что они пахнут женщиной, о чем говорила и Коко Шанель.";
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
                shopViewModel.Brands.Add(new MenuBrandViewModel
                {
                    Name = brand.Name,
                    Id = brand.BrandId.ToString(),
                    IsChecked = request.SelectedBrands != null && request.SelectedBrands.Contains(brand.BrandId.ToString())
                });
            }


          //  string webRootPath = _env.WebRootPath;

          //  string imagePath = "~/img/product-img/product";

            //string imagePath1 = "/img/product-img/product";
            //var path = Path.Combine(_env.WebRootPath, imagePath1);


            //string p = HttpContext.Current.Server.MapPath("/UploadedFiles");

            int selectedQuantityOnPage = request.SelectedQuantityOnPage;
            int selectedSort = request.SelectedSort;
            int selectedPage = request.SelectedPage;

            List<Product> products = null;
          
            if (request.SelectedBrands != null && request.SelectedGender != null)
                products = db.Products.Where(p => (request.SelectedBrands.Contains(p.BrandId.ToString())
                && p.Gender == request.SelectedGender)
                && p.Price <= request.PriceMax && p.Price >= request.PriceMin).ToList();

            else if (request.SelectedBrands != null && request.SelectedGender == null)
             
                    products = db.Products.Where(p => request.SelectedBrands.Contains(p.BrandId.ToString())
                 && p.Price <= request.PriceMax && p.Price >= request.PriceMin).ToList();
                
            else if (request.SelectedBrands == null && request.SelectedGender != null)
                products = db.Products.Where(p => (p.Gender == request.SelectedGender)
                 && p.Price <= request.PriceMax && p.Price >= request.PriceMin).ToList();
            else
                products = db.Products.Where(p => p.Price <= request.PriceMax && p.Price >= request.PriceMin).ToList();

            shopViewModel.QuantityOfPages = (int)Math.Ceiling(products.Count / (double)selectedQuantityOnPage);

            shopViewModel.TotalProductsCount = products.Count;
           
            Random rnd = new Random();
            foreach (var product in products)
            {
                var shortProductViewModel = new ShortProductViewModel();

                shortProductViewModel.Name = product.Name;
                shortProductViewModel.Price = (int)product.Price;

                string directoryPath = Path.Combine(_env.WebRootPath, "img", "prod-img", product.ProductId.ToString(), "small");
                string imagePathRel = String.Concat("~/img/prod-img/", product.ProductId.ToString(), "/small");

                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath);
                    string fileNameShort;
                    if (files.Length > 0)
                    {
                        fileNameShort = files[0].Replace(directoryPath + "\\", "");
                        shortProductViewModel.ImageUrl = imagePathRel + "/" + fileNameShort;
                        if (files.Length > 1)
                        {
                            fileNameShort = files[1].Replace(directoryPath + "\\", "");
                            shortProductViewModel.HoverImageUrl = imagePathRel + "/" + fileNameShort;

                        }
                        else shortProductViewModel.HoverImageUrl = shortProductViewModel.ImageUrl;
                    }
                    else
                    { shortProductViewModel.ImageUrl = "~/img/prod-img/product1";
                      shortProductViewModel.HoverImageUrl = "~/img/prod-img/product1";
                    }
                    
                }

                shortProductViewModel.Rating = rnd.Next(1, 5);
                shortProductViewModel.ProductId = product.ProductId;

                shopViewModel.Products.Add(shortProductViewModel);



            }

            if (selectedSort == 1)
            {
                //var ps = shopViewModel.Products;
                //ps = (from p in ps
                //      orderby p.Price
                //      select p).ToList();

                //shopViewModel.Products = ps;

                shopViewModel.Products = shopViewModel.Products.OrderBy(p => p.Price).ToList();
            }
             
            else if (selectedSort == 2)
            {
                shopViewModel.Products = shopViewModel.Products.OrderByDescending(p => p.Price).ToList();
            }
           
            else if (selectedSort == 3)
            {
                shopViewModel.Products = shopViewModel.Products.OrderByDescending(p => p.Rating).ToList();
            }

            shopViewModel.Products = shopViewModel.Products.Skip((selectedPage - 1) * selectedQuantityOnPage).Take(selectedQuantityOnPage).ToList();

            shopViewModel.SelectedGender = request.SelectedGender;

            shopViewModel.PriceMin = request.PriceMin;
            shopViewModel.PriceMax = request.PriceMax;
            shopViewModel.SelectedQuantityOnPage = selectedQuantityOnPage;
            shopViewModel.SelectedPage = selectedPage;
            shopViewModel.SelectedSort = selectedSort;
            return View(shopViewModel);

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
