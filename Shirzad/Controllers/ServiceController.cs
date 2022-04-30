using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.publicClasses;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _context;
        public ServiceController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var services = await _context.serviceUW.GetEntitiesAsync();
            return View(services);
        }

        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(Service service, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(service);

            string imgname = UploadFiles.CreateImg(file, "Service");

            service.PhotoUrl = imgname;
            await _context.serviceUW.Create(service);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            Service service = await _context.serviceUW.GetByIdAsync(id);
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service service, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }
            if (file != null)
            {
                string imgname = UploadFiles.CreateImg(file, "Service");

                bool DeleteImage = UploadFiles.DeleteImg("Service", service.PhotoUrl);
                service.PhotoUrl = imgname;
            }
            _context.serviceUW.Update(service);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Service service = await _context.serviceUW.GetByIdAsync(id);
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Service service)
        {
            UploadFiles.DeleteImg("Service", service.PhotoUrl);

            await _context.serviceUW.DeleteByIdAsync(service.Id);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
