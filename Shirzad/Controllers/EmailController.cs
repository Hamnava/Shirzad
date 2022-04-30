using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly INotyfService _notify;
        private readonly IEmailRepository _product;
        private readonly UserManager<ApplicationUser> _userManager;
      
        public EmailController(IUnitOfWork context, INotyfService notfy, 
            IEmailRepository product, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notify = notfy;
            _product = product;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var emails = await _context.emailRegisterUW.GetEntitiesAsync(null,
                         x=> x.OrderByDescending(p=> p.Id));
            return View(emails);
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
                    await _product.SendEmailAsync(item.Email, model.Subject, model.Message, user.UserName, user.EmailPassword, user.Email);
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmail(string emailaddress)
        {
            if ( await _product.IsEmailExisted(emailaddress))
            {
                _notify.Success("Your Email Registered Successfuly", 5);
                return RedirectToAction("Index", "SitePages");
            }
            else
            {
                var emailAddress = new EmailRegister
                {
                    Email = emailaddress
                };
                await _context.emailRegisterUW.Create(emailAddress);
                await _context.saveAsync();
                _notify.Success("Your Email Registered Successfuly", 5);
                return RedirectToAction("Index", "SitePages");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.emailRegisterUW.DeleteByIdAsync(id);
            await _context.saveAsync();
            _notify.Success("The Email Deleleted Successfuly", 5);
            return RedirectToAction("Index");
        }
    }
}
