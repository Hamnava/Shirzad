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
    public class ProductGalleryController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductGalleryController(IUnitOfWork context, IMapper mapper, IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;   
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var product = await _context.productUW.GetEntitiesAsync();
            var mapModel = _mapper.Map<IEnumerable<ProductViewModel>>(product);
            return View(mapModel);
            
        }

        public async Task<IActionResult> AddPhoto(int id)
        {
            var product = await _context.productUW.GetByIdAsync(id);

            ViewBag.ProductId = id;
            ViewBag.PhotoUrl = product.PhotoUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPhoto(IFormFile file, GalleryViewModel gallery)
        {
            if (file != null)
            {
                string imgname = UploadFiles.CreateImg(file, "Product");
                if (imgname == "false")
                {
                    return View(gallery);
                }
                var photo = new ProductGallery
                {
                    Url =  imgname,
                    ProductId = gallery.ProductId
                };
               
                await _context.galleryUW.Create(photo);
                await _context.saveAsync();
                return RedirectToAction(nameof(AddPhoto));
            }
            return View(gallery);
        }

        
        public async Task<IActionResult> Delete(int id, string url, int productId)
        {
            var pic = await _context.galleryUW.GetByIdAsync(id);
            UploadFiles.DeleteImg("Product", pic.Url);
            await _context.galleryUW.DeleteByIdAsync(id);
            await _context.saveAsync();
            ViewBag.ProductId = productId;
            ViewBag.PhotoUrl = url;
            return RedirectToAction(nameof(Index));
        }
    }
}
