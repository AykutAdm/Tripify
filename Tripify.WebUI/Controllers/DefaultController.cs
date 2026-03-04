using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
