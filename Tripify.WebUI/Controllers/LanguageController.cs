using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Tripify.WebUI.Controllers
{
    public class LanguageController : Controller
    {
        [HttpPost]
        public IActionResult Change(string culture, string returnUrl)
        {
            var supportedCultures = new[] { "tr", "en", "de", "fr", "es" };
            if (string.IsNullOrEmpty(culture) || !supportedCultures.Contains(culture))
                culture = "tr";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    Path = "/",
                    SameSite = SameSiteMode.Lax
                });

            return LocalRedirect(returnUrl ?? "/Default/Index");
        }
    }
}
