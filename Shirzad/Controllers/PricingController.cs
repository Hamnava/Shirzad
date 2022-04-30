using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class PricingController : Controller
    {
        private readonly IUnitOfWork _context;
        public PricingController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.pricingUW.GetEntitiesAsync(null, x => x.OrderByDescending(c => c.Id));
            return View(orders);
        }

       
        public async Task<IActionResult> Detials(int id)
        {
            var order = await _context.pricingUW.GetByIdAsync(id);
            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {

            await _context.pricingUW.DeleteByIdAsync(id);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
