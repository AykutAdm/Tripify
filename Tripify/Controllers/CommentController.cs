using Microsoft.AspNetCore.Mvc;

namespace Tripify.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
