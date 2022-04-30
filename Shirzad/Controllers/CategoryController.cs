using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _context;
        public CategoryController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.categoryUW.GetEntitiesAsync());
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = viewModel.Name,
                    IsDelete = false
                };
                await _context.categoryUW.Create(category);
                await _context.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.categoryUW.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.categoryUW.Update(model);
                await _context.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.categoryUW.GetByIdAsync(id);
            if (category.IsDelete)
            {
                ViewBag.Message = "Your are actvating this Category!";
            }
            else
            {
            ViewBag.Message = "Your are InActvating this Category! , you can always undo";
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsDelete)
                {
                    model.IsDelete = false;
                }
                else
                {
                    model.IsDelete = true;
                }
                _context.categoryUW.Update(model);
                await _context.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


    }
}
