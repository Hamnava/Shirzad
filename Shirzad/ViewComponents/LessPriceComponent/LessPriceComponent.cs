using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Entities;

namespace Shirzad.ViewComponents
{
    public class LessPriceComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public LessPriceComponent(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LessPrice", await _context.productUW.GetEntitiesAsync(null, x=> (IOrderedQueryable<Product>) x.OrderBy(p=> p.Price).Take(5))));
        }
    }
}
