using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        [Route("account/login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/login")]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {

            }
            return View(loginModel);
        }
        [Route("account/register")]
        public IActionResult Register()
        {
            return View();
        }

    }
}