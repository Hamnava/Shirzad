using Microsoft.AspNetCore.Mvc;

namespace Shirzad.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
