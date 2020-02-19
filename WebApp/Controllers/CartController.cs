using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataLibrary.DataAccess;
using EFDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UndefinedContext _db;
        private Cart _cart;
        public CartController(ILogger<HomeController> logger, UndefinedContext db,Cart cart)
        {
            _logger = logger;
            _db = db;
            _cart = cart;
        }
        [Route("cart/index")]
        public IActionResult Index()
        {
            return View(_cart.Lines);
        }
        [Route("product/{name?}")]
        public IActionResult Details(int id,string name,string returnUrl)
        {
            Console.WriteLine(_cart.CartId);
            ViewBag.id = id;
            return View(_db.Photos.FirstOrDefault(ph => ph.Id == 39));
        }
        [HttpGet]
        [Route("add/{id?}/{quantity?}")]
        public string AddToCart(int id,int quantity)
        {
            foreach (var res in Request.Query)
            {
                Console.WriteLine(res.Value);
            }
            Console.WriteLine($"{id}, {quantity}");
            if (quantity > 0 && quantity < 10)
            {
                Product product = _db.Products.FirstOrDefault(p => p.Id == id);
                _cart.AddLine(product, quantity);
                return "OK";
            }
            else return "quantity is incorrect";
        }
    }
}