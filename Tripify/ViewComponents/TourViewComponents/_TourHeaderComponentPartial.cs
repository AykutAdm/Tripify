using Microsoft.AspNetCore.Mvc;

namespace Tripify.ViewComponents.TourViewComponents
{
    public class _TourHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
