using Microsoft.AspNetCore.Mvc;

namespace Unidad3P1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
