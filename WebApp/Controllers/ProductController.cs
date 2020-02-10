using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EFDataLibrary.DataAccess;
using EFDataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using System.IO;
namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UndefinedContext _db;
        private int PageSize = 12;
        public ProductController(ILogger<HomeController> logger,UndefinedContext db)
        {
            _logger = logger;
            _db = db;
        }
        List<List<Product>> Products = new List<List<Product>>();
        public IActionResult List(string category,int page = 1)
        {
            LoadSampleData();
            ImportPhoto();
            List<Product> products = new List<Product>();
            for (int i = 0; i< _db.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).Count();i++)
            {
                products.Add(_db.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).ToArray()[i]);
                if(i+1== _db.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).Count())
                {
                    Products.Add(products);
                    products = new List<Product>();
                    break;
                }
                if((i+1)%3==0)
                {
                    Products.Add(products);
                    products = new List<Product>();
                }
            }
            ProductListViewModel model = new ProductListViewModel()
            {
                products = Products,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null?
                    _db.Products.Count(): _db.Products.Where(p=>p.Category==category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        private void LoadSampleData()
        {
            if (_db.Products.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated.json");
                var people = JsonSerializer.Deserialize<List<Product>>(file);
                _db.AddRange(people);  
                _db.SaveChanges();
            }
        }
        public void ImportPhoto()
        {
            string filename = @"wwwroot\images\si1.jpg";
            byte[] image = null;
            using (var binaryReader = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                image = binaryReader.ReadBytes((int)(new FileInfo(filename).Length));
            }
            _db.Photos.Add(new Photo() { Image = image });
            _db.SaveChanges();
        }
    }
}