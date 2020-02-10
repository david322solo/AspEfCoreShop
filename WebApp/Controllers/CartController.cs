using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UndefinedContext _db;
        public CartController(ILogger<HomeController> logger, UndefinedContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("product/{name?}")]
        public IActionResult Details(int id,string name,string returnUrl)
        {
            return View(_db.Photos.FirstOrDefault(ph => ph.Id == 16));
        }
    }
}