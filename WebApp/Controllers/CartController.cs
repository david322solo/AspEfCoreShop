using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("product/{name?}")]
        public IActionResult Details(int id,string name,string returnUrl)
        {
            Console.WriteLine($" {id}, {name}, {returnUrl}");
            return View();
        }
    }
}