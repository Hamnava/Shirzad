using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.ServiceComponent
{
    public class ServiceComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public ServiceComponent(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Service", await _context.serviceUW.GetEntitiesAsync()));
        }
    }
}
