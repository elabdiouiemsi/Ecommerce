using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
