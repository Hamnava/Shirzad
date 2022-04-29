using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Shirzad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}