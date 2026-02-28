using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.TourViewComponents
{
    public class _TourScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
