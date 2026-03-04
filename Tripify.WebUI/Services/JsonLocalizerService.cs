using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;

namespace Tripify.WebUI.Services
{
    public class JsonLocalizerService : ILocalizerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> _cache = new();
        private static readonly string[] SupportedCultures = { "tr", "en", "de", "fr", "es" };

        public JsonLocalizerService(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        private string GetCurrentCulture()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[".AspNetCore.Culture"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var parts = cookie.Split('|');
                foreach (var part in parts)
                {
                    var p = part.Trim();
                    if (p.StartsWith("c="))
                    {
                        var culture = p[2..].Trim().Trim('\'');
                        if (!string.IsNullOrEmpty(culture) && SupportedCultures.Contains(culture))
                            return culture;
                    }
                    if (p.StartsWith("uic="))
                    {
                        var culture = p[4..].Trim().Trim('\'');
                        if (!string.IsNullOrEmpty(culture) && SupportedCultures.Contains(culture))
                            return culture;
                    }
                }
            }
            return "tr";
        }

        private Dictionary<string, string> LoadLocale(string culture)
        {
            return _cache.GetOrAdd(culture, c =>
            {
                var path = Path.Combine(_env.WebRootPath, "locales", $"{c}.json");
                if (!File.Exists(path))
                    return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                try
                {
                    var json = File.ReadAllText(path);
                    var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    return dict ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                catch
                {
                    return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
            });
        }

        public string this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key)) return key;
                var culture = GetCurrentCulture();
                var locale = LoadLocale(culture);
                if (locale.TryGetValue(key, out var value))
                    return value;
                locale = LoadLocale("tr");
                return locale.TryGetValue(key, out value) ? value : key;
            }
        }
    }
}
