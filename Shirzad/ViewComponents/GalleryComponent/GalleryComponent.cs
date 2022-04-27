using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.GalleryComponent
{
    public class GalleryComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public GalleryComponent(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("GalleryAdmin", await _context.galleryUW.GetEntitiesAsync(x=> x.ProductId == id)));
        }
    }
}
