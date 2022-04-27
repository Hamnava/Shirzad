using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.BestSaller
{
    public class BestSaller: ViewComponent
    {
        private readonly IUnitOfWork _context;
        public BestSaller(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("BestSaller", await _context.productUW.GetEntitiesAsync(x => x.BestSaller && x.IsActive)));
        }
    }
}
