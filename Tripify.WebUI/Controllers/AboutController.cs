using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
