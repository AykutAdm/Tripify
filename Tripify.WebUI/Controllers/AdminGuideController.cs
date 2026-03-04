using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripify.WebUI.Dtos.GuideDtos;

namespace Tripify.WebUI.Controllers
{
    public class AdminGuideController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminGuideController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GuideList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Guides");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGuideDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateGuide()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuide(CreateGuideDto createGuideDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createGuideDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Guides", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GuideList");
            }
            return View();
        }

        public async Task<IActionResult> RemoveGuide(string id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7250/api/Guides/" + id);
            return RedirectToAction("GuideList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGuide(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Guides/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetGuideByIdDto>(jsonData);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGuide(UpdateGuideDto updateGuideDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateGuideDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7250/api/Guides", stringContent);
            return RedirectToAction("GuideList");
        }
    }
}

