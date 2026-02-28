using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.TourViewComponents
{
    public class _TourFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
