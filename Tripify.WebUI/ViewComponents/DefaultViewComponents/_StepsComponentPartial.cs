using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.ViewComponents.DefaultViewComponents
{
    public class _StepsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
