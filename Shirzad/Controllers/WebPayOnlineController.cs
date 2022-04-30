using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.publicClasses;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class WebPayOnlineController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public WebPayOnlineController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.webPayOnlineUW.GetEntitiesAsync());
        }

        public IActionResult AddWebPay()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWebPay(WebPayOnlineViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(model);

            string imgname = UploadFiles.CreateImg(file, "WebPay");

            model.PhotoUrl = imgname;
            var mapModel = _mapper.Map<WebPayOnline>(model);
            await _context.webPayOnlineUW.Create(mapModel);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var webPay = await _context.webPayOnlineUW.GetByIdAsync(id);
            return View(webPay);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WebPayOnlineViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (file != null)
            {
                string imgname = UploadFiles.CreateImg(file, "WebPay");
                bool DeleteImage = UploadFiles.DeleteImg("WebPay", model.PhotoUrl);
                model.PhotoUrl = imgname;
            }
            var webPay = await _context.webPayOnlineUW.GetByIdAsync(model.Id);
            var mapModel = _mapper.Map(model, webPay);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
