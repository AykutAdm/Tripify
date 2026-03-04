using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.TourDetailViewComponents
{
    public class _TourDetailVideoComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
