using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.RelatedProduct
{
    public class RelatedProduct : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public RelatedProduct(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("RelatedProduct", await _context.productUW.GetEntitiesAsync(x=> x.CategoryId == id)));
        }
    }
}
