using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tripify.WebUI.Dtos.BookingDtos;
using Tripify.WebUI.Dtos.TourDtos;

namespace Tripify.WebUI.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var toursResponse = await client.GetAsync("https://localhost:7250/api/Tours");
            var tours = new List<ResultTourDto>();
            if (toursResponse.IsSuccessStatusCode)
            {
                var json = await toursResponse.Content.ReadAsStringAsync();
                tours = JsonConvert.DeserializeObject<List<ResultTourDto>>(json);
            }

            var bookingsResponse = await client.GetAsync("https://localhost:7250/api/Bookings");
            var bookings = new List<ResultBookingDto>();
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var json = await bookingsResponse.Content.ReadAsStringAsync();
                bookings = JsonConvert.DeserializeObject<List<ResultBookingDto>>(json);
            }

            var commentsResponse = await client.GetAsync("https://localhost:7250/api/Comments");
            var comments = new List<object>();
            if (commentsResponse.IsSuccessStatusCode)
            {
                var json = await commentsResponse.Content.ReadAsStringAsync();
                comments = JsonConvert.DeserializeObject<List<object>>(json) ?? comments;
            }

            var testimonialsResponse = await client.GetAsync("https://localhost:7250/api/Testimonials");
            var testimonials = new List<object>();
            if (testimonialsResponse.IsSuccessStatusCode)
            {
                var json = await testimonialsResponse.Content.ReadAsStringAsync();
                testimonials = JsonConvert.DeserializeObject<List<object>>(json);
            }

            var guidesResponse = await client.GetAsync("https://localhost:7250/api/Guides");
            var guides = new List<object>();
            if (guidesResponse.IsSuccessStatusCode)
            {
                var json = await guidesResponse.Content.ReadAsStringAsync();
                guides = JsonConvert.DeserializeObject<List<object>>(json);
            }

            ViewBag.ToursCount = tours.Count;
            ViewBag.BookingsCount = bookings.Count;
            ViewBag.CommentsCount = comments.Count;
            ViewBag.TestimonialsCount = testimonials.Count;
            ViewBag.GuidesCount = guides.Count;
            ViewBag.RecentTours = tours.OrderByDescending(x => x.TourDate).Take(5).ToList();
            ViewBag.RecentBookings = bookings.OrderByDescending(x => x.BookingDate).Take(8).ToList();

            return View();
        }
    }
}
