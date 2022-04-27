using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> sin, UserManager<ApplicationUser> userManager)
        {
            _signInManager = sin;
            _userManager = userManager;
        }
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = await _userManager.FindByNameAsync(model.UserName);
                if (username == null)
                {
                    ModelState.AddModelError("Password", "Oops, Incorrect username or password 🙄");
                    return View(model);
                }
                
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.Ispersistant, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("Password", "Oops, Incorrect username or password 🙄");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
