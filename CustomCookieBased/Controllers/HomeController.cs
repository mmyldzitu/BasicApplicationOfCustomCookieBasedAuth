using CustomCookieBased.Data;
using CustomCookieBased.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomCookieBased.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _myContext;

        public HomeController(MyContext myContext)
        {
            _myContext = myContext;
        }

       public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View(new UserSignInModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var user = _myContext.Users.SingleOrDefault(x => x.userName == model.userName && x.Password == model.Password);
            if (user != null)
            {
                var roles = _myContext.Roles.Where(x => x.userRoles.Any(x => x.userID == user.Id)).Select(x => x.Definition).ToList();
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,model.userName) };
                foreach ( var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

            
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.rememberMe
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");

            return View(model);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public IActionResult Member()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
