using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EFDataLibrary.DataAccess;
using EFDataLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private UndefinedContext _db;
        public AccountController(UndefinedContext db)
        {
            _db = db;
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                User user = await _db.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.Username == loginModel.Username &&
                u.Password == loginModel.Password);
                if (user != null)
                {
                    await Authenticate(user); 

                    return RedirectToAction("List", "Product");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(loginModel);
        }
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Username == registerModel.Username && 
                    u.Email == registerModel.Email);
                if (user == null)
                {
                    if (registerModel.Password == registerModel.ConfirmPassword)
                    {
                        _db.Users.Add(
                        new User()
                        {
                            Name = registerModel.Name,
                            Password = registerModel.Password,
                            Email = registerModel.Email,
                            Surname = registerModel.Surname,
                            Username = registerModel.Username
                        });
                        await _db.SaveChangesAsync();
                        await Authenticate(new User());
                        return RedirectToAction("List", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пароли не совпадают");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(registerModel);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
            new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role?.Name)
        };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}