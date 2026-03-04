using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripify.WebUI.Dtos.BookingDtos;
using Tripify.WebUI.Dtos.TourDtos;

namespace Tripify.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string tourId)
        {

            // Tur bilgilerini çek
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Tours/{tourId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var tour = JsonConvert.DeserializeObject<GetTourByIdDto>(jsonData);

                ViewBag.Tour = tour;
                ViewBag.TourId = tourId;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.BookingDate = DateTime.Now;
            createBookingDto.Status = "Pending"; // Beklemede

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Bookings", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rezervasyonunuz başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Default");
            }

            TempData["ErrorMessage"] = "Rezervasyon oluşturulurken bir hata oluştu.";
            return RedirectToAction("Index", new { tourId = createBookingDto.TourId });
        }
    }
}
