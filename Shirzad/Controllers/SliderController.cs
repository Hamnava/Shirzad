using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.publicClasses;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    public class SliderController : Controller
    {
        private readonly IUnitOfWork _context;
        public SliderController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.sliderUW.GetEntitiesAsync());
        }

        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider(Slider Slider, IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(Slider);

            string imgname = UploadFiles.CreateImg(file, "Slider");

            Slider.SliderImg = imgname;
            await _context.sliderUW.Create(Slider);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            Slider mainSlider = await _context.sliderUW.GetByIdAsync(id);
            return View(mainSlider);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (file != null)
            {
                string imgname = UploadFiles.CreateImg(file,"Slider");

                bool DeleteImage = UploadFiles.DeleteImg("Slider",slider.SliderImg);
                slider.SliderImg = imgname;
            }
            _context.sliderUW.Update(slider);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Slider mainSlider = await _context.sliderUW.GetByIdAsync(id);
            return View(mainSlider);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Slider mainSlider)
        {
            UploadFiles.DeleteImg("Slider",mainSlider.SliderImg);

            await _context.sliderUW.DeleteByIdAsync(mainSlider.Id);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
