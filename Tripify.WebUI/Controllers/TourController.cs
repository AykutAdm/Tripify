using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Tripify.WebUI.Dtos.CommentDtos;
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

        public IActionResult Index()
        {
            return RedirectToAction("TourList");
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

        public async Task<IActionResult> TourDetail(string id)
        {
            ViewBag.TourId = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Tours/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var tour = JsonConvert.DeserializeObject<GetTourByIdDto>(jsonData);
                ViewBag.Tour = tour;
            }
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> MakeComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.CommentDate = DateTime.Now;
            createCommentDto.IsStatus = true;
            createCommentDto.Score = 5;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Comments", stringContent);

            return RedirectToAction("TourDetail", new { id = createCommentDto.TourId });
        }
    }
}
