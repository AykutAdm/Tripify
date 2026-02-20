using Microsoft.AspNetCore.Mvc;

namespace Tripify.ViewComponents.TourViewComponents
{
    public class _TourFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
