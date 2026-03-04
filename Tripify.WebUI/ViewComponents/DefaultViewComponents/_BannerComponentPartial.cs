using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.DefaultViewComponents
{
    public class _BannerComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
