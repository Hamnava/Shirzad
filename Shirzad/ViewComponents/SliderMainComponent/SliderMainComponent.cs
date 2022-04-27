using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;

namespace Shirzad.ViewComponents.SliderMainComponent
{
    public class SliderMainComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public SliderMainComponent(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("MainSlider", await _context.sliderUW.GetEntitiesAsync(null, s=> s.OrderBy(x=> x.SliderSort))));
        }
    }
}
