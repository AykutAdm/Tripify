using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using System.Text;
using Tripify.WebUI.Dtos.BookingDtos;
using Tripify.WebUI.Dtos.MailDtos;
using Tripify.WebUI.Dtos.TourDtos;
using MimeKit;


namespace Tripify.WebUI.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> BookingList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Bookings");

            if (!responseMessage.IsSuccessStatusCode)
            {
                return View(new List<ResultBookingDto>());
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData) ?? new List<ResultBookingDto>();

            // Tur başlığını gösterebilmek için gerekli Id'leri topla
            var tourIds = values.Select(x => x.TourId).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            var tourTitles = new Dictionary<string, string>();

            foreach (var tourId in tourIds)
            {
                var tourResponse = await client.GetAsync($"https://localhost:7250/api/Tours/{tourId}");
                if (!tourResponse.IsSuccessStatusCode) continue;

                var tourJson = await tourResponse.Content.ReadAsStringAsync();
                var tour = JsonConvert.DeserializeObject<GetTourByIdDto>(tourJson);
                if (tour != null)
                {
                    tourTitles[tourId] = tour.Title;
                }
            }

            ViewBag.TourTitles = tourTitles;

            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string id, string status)
        {
            var client = _httpClientFactory.CreateClient();

            // Mevcut rezervasyonu çek
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Bookings/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingList");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);
            if (booking == null)
            {
                return RedirectToAction("BookingList");
            }

            booking.Status = status;

            var updateJson = JsonConvert.SerializeObject(booking);
            StringContent stringContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7250/api/Bookings", stringContent);

            return RedirectToAction("BookingList");
        }

        [HttpGet]
        public async Task<IActionResult> StatusMail(string id, string status)
        {
            var client = _httpClientFactory.CreateClient();

            // Booking detayını çek
            var bookingResp = await client.GetAsync($"https://localhost:7250/api/Bookings/{id}");
            if (!bookingResp.IsSuccessStatusCode)
                return RedirectToAction("BookingList");

            var bookingJson = await bookingResp.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<GetBookingByIdDto>(bookingJson);

            // OpenAI'den mail taslağı iste
            var emailResp = await client.GetAsync($"https://localhost:7250/api/Bookings/{id}/status-email?status={status}");
            if (!emailResp.IsSuccessStatusCode)
                return RedirectToAction("BookingList");

            dynamic emailData = JsonConvert.DeserializeObject(emailResp.Content.ReadAsStringAsync().Result)!;

            var model = new MailRequestDto
            {
                BookingId = id,
                Status = status,
                ReceiverEmail = booking.Email,
                Subject = (string)emailData.subject,
                MessageDetail = (string)emailData.body
            };

            ViewBag.Booking = booking; // ekranda göstermek istersen

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StatusMail(MailRequestDto mailRequest)
        {
            // 1) Mail gönder
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tripify", "your-mail@gmail.com"));
            message.To.Add(new MailboxAddress("Kullanıcı", mailRequest.ReceiverEmail));
            message.Subject = mailRequest.Subject;

            var bodyBuilder = new BodyBuilder { TextBody = mailRequest.MessageDetail };
            message.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, false);
                await smtp.AuthenticateAsync("your-mail@gmail.com", "your-key");
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }

            // 2) Booking status güncelle
            var client = _httpClientFactory.CreateClient();

            var bookingResp = await client.GetAsync($"https://localhost:7250/api/Bookings/{mailRequest.BookingId}");
            if (bookingResp.IsSuccessStatusCode)
            {
                var bookingJson = await bookingResp.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<UpdateBookingDto>(bookingJson);

                if (booking != null)
                {
                    booking.Status = mailRequest.Status;
                    var jsonData = JsonConvert.SerializeObject(booking);
                    var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await client.PutAsync("https://localhost:7250/api/Bookings", stringContent);
                }
            }

            TempData["SuccessMessage"] = "Mail gönderildi ve rezervasyon durumu güncellendi.";
            return RedirectToAction("BookingList");
        }
    }
}

