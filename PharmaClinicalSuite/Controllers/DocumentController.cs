using Microsoft.AspNetCore.Mvc;

namespace PharmaClinicalSuite.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
