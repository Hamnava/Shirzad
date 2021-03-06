using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.ViewModels;
using Shirzad.DataLayer.Entities;

namespace Shirzad.Controllers
{
    public class SitePagesController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;
        private readonly IProductRepository _product;
        public SitePagesController(IUnitOfWork context, IMapper mapper, INotyfService notify, IProductRepository product)
        {
            _context = context;
            _mapper = mapper;
            _notify = notify;
            _product = product;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.categoryUW.GetEntitiesAsync();
            return View(categories);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.productUW.GetByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ProductSearch(string text, List<int> categoryid, int sort = 1)
        {
            ViewBag.categories = await _context.categoryUW.GetEntitiesAsync();
            ViewBag.text = text;
            ViewBag.sort = sort;
            ViewBag.categoryid = categoryid;
            if (text == null ||text== "")
            {
                var product = await _context.productUW.GetEntitiesAsync();
                return View(product);
            }
            var products = _product.Search(text, categoryid, sort);
            return View(products);
        }

        public IActionResult PricingOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PricingOrder(PricingViewModel pricing)
        {
            if (!ModelState.IsValid) return View(pricing);
            var mapModel = _mapper.Map<Pricing>(pricing);
            await _context.pricingUW.Create(mapModel);
            await _context.saveAsync();
            _notify.Success("Your message was sent successfuly!");
            return RedirectToAction(nameof(PricingOrder));
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel contact)
        {
            if (!ModelState.IsValid) return View(contact);
            var mapModel = _mapper.Map<ContactUs>(contact);
            await _context.contactUsUW.Create(mapModel);
            await _context.saveAsync();
            _notify.Success("Your message was sent successfuly!");
            return RedirectToAction(nameof(Contact));
        }

        public async Task<IActionResult> WebPay()
        {
            var webPay = await _context.webPayOnlineUW.GetEntitiesAsync();
            return View(webPay);
        }
    }
}
