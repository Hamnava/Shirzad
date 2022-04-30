using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shirzad.Core.publicClasses;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(IUnitOfWork context, IMapper mapper, IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.productUW.GetEntitiesAsync(null, null, "Category"));
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
                return View(model);
            }

            if(await _productRepository.GetProductByProductNameAsync(model.Name, 0))
            {
                ModelState.AddModelError("Name", "This product is already available in the system!");
                ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
                return View(model);
            }

            if (file == null)
            {
                ModelState.AddModelError("PhotoUrl", "Please Choose an image for product");
                ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
                return View(model);
            }
            var imagename = UploadFiles.CreateImg(file, "Product");
            var mapModel = _mapper.Map<Product>(model);
            mapModel.PhotoUrl = imagename;
            await _context.productUW.Create(mapModel);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
            Product product = await _context.productUW.GetByIdAsync(id);
            var mapUser = _mapper.Map<ProductViewModel>(product);
            return View(mapUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
                return View(model);
            }
            if (await _productRepository.GetProductByProductNameAsync(model.Name, model.Id))
            {
                ModelState.AddModelError("Name", "This product is already available in the system!");
                ViewBag.CategoryId = new SelectList(await _context.categoryUW.GetEntitiesAsync(), "Id", "Name");
                return View(model);
            }
            if (file != null)
            {
                string imgname = UploadFiles.CreateImg(file, "Product");

                bool DeleteImage = UploadFiles.DeleteImg("Product", model.PhotoUrl);
                model.PhotoUrl= imgname;
            }
            var product = await _context.productUW.GetByIdAsync(model.Id);
            var mapModel = _mapper.Map(model, product);
            
            _context.productUW.Update(mapModel);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.productUW.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product model)
        {
            UploadFiles.DeleteImg("Product", model.PhotoUrl);
            await _context.productUW.DeleteByIdAsync(model.Id);
            await _context.saveAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
