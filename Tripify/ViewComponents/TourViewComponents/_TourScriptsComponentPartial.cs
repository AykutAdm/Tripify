using Microsoft.AspNetCore.Mvc;

namespace Tripify.ViewComponents.TourViewComponents
{
    public class _TourScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
