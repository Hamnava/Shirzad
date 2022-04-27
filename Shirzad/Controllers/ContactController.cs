using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _context;
        public ContactController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.contactUsUW.GetEntitiesAsync(null, x=> x.OrderByDescending(c=> c.Id));
            return View(contacts);
        }

        public async Task<IActionResult> Detials(int id)
        {
            var contact = await _context.contactUsUW.GetByIdAsync(id);
            return View(contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _context.contactUsUW.DeleteByIdAsync(id);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
