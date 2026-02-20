using Microsoft.AspNetCore.Mvc;

namespace Tripify.Controllers
{
    public class AdminTourController : Controller
    {
        public IActionResult TourList()
        {
            return View();
        }
    }
}
