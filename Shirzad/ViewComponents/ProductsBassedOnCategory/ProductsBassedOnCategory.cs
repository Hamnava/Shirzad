using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.BestSaller
{
    public class ProductsBassedOnCategory: ViewComponent
    {
        private readonly IUnitOfWork _context;
        public ProductsBassedOnCategory(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("ProductsBassedOnCategory", await _context.productUW.GetEntitiesAsync(x => x.CategoryId == id)));
        }
    }
}
