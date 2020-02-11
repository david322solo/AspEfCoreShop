using EFDataLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Components
{
    public class MenuViewComponent: ViewComponent
    {
        private UndefinedContext _db;
        public MenuViewComponent(UndefinedContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var elements = _db.Products.GroupBy(p => p.Category).Select(p => new MenuViewModel { Category = p.Key, Count = p.Count() });
           
            return View(elements);
        }
    }
}
