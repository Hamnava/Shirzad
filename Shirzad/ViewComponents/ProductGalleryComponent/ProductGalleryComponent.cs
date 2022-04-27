using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.ProductGalleryComponent
{
    public class ProductGalleryComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public ProductGalleryComponent(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("GalleryProduct", await _context.galleryUW.GetEntitiesAsync(x => x.ProductId == id, null, "Product")));
        }
    }
}
