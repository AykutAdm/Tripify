using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripify.WebUI.Dtos.TourDtos;

namespace Tripify.WebUI.Controllers
{
    public class TourController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TourController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateTour()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTourDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Tours", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TourList");
            }
            return View();
        }

        public async Task<IActionResult> TourList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Tours");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTourDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
