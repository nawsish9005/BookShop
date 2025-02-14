using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
