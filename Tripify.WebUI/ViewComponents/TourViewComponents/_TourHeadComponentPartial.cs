using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.TourViewComponents
{
    public class _TourHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
