using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _email;
        public AccountController(SignInManager<ApplicationUser> sin,IEmailRepository email, IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork context)
        {
            _signInManager = sin;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _email = email;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailSenderViewModel model)
        {
           string UserName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(UserName);
            var emaillists = await _context.emailRegisterUW.GetEntitiesAsync();
            if (emaillists != null)
            {
                foreach (var item in emaillists)
                {
                    await _email.SendEmailAsync(item.Email, model.Subject, model.Message, user.UserName, user.EmailPassword, user.Email);
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var user = await _context.userManagerUW.GetEntitiesAsync();
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _context.userManagerUW.GetByIdAsync(id);
            var mapUser = _mapper.Map<UserViewModel>(user);
            return View(mapUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //update
                var user = await _userManager.FindByIdAsync(model.Id);
                IdentityResult result = await _userManager.UpdateAsync(_mapper.Map(model, user));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            return View(model);
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
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("Password", "Oops, Incorrect username or password 🙄");
                    return View(model);
                }
                
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.Ispersistant, lockoutOnFailure: false);
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
