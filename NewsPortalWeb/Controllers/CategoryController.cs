using Microsoft.AspNetCore.Mvc;

namespace NewsPortalWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
