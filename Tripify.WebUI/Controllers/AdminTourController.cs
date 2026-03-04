using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using SelectPdf;
using Tripify.WebUI.Dtos.BookingDtos;
using Tripify.WebUI.Dtos.TourDtos;


namespace Tripify.WebUI.Controllers
{
    public class AdminTourController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminTourController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

        public async Task<IActionResult> RemoveTour(string id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7250/api/Tours/" + id);
            return RedirectToAction("TourList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTour(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Tours/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetTourByIdDto>(jsonData);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateTourDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7250/api/Tours/", stringContent);
            return RedirectToAction("TourList");
        }

        [HttpGet]
        public async Task<IActionResult> TourBookings(string id)
        {
            var client = _httpClientFactory.CreateClient();
            ViewBag.TourId = id;
            var tourResponse = await client.GetAsync($"https://localhost:7250/api/Tours/{id}");
            if (tourResponse.IsSuccessStatusCode)
            {
                var tourJson = await tourResponse.Content.ReadAsStringAsync();
                var tour = JsonConvert.DeserializeObject<GetTourByIdDto>(tourJson);
                ViewBag.TourTitle = tour?.Title ?? "Tur";
            }

            var bookingsResponse = await client.GetAsync($"https://localhost:7250/api/Bookings/tour/{id}");
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var bookingsJson = await bookingsResponse.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<ResultBookingDto>>(bookingsJson);
                return View(bookings ?? new List<ResultBookingDto>());
            }

            return View(new List<ResultBookingDto>());
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTourBookingsPdf(string id)
        {
            var client = _httpClientFactory.CreateClient();
            string tourTitle = "Tur";

            var tourResponse = await client.GetAsync($"https://localhost:7250/api/Tours/{id}");
            if (tourResponse.IsSuccessStatusCode)
            {
                var tourJson = await tourResponse.Content.ReadAsStringAsync();
                var tour = JsonConvert.DeserializeObject<GetTourByIdDto>(tourJson);
                tourTitle = tour?.Title ?? tourTitle;
            }

            var bookingsResponse = await client.GetAsync($"https://localhost:7250/api/Bookings/tour/{id}");
            var bookings = new List<ResultBookingDto>();
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var bookingsJson = await bookingsResponse.Content.ReadAsStringAsync();
                bookings = JsonConvert.DeserializeObject<List<ResultBookingDto>>(bookingsJson);
            }

            var encodedTourTitle = WebUtility.HtmlEncode(tourTitle);
            var safeFileName = string.Join("_", tourTitle.Split(Path.GetInvalidFileNameChars()));
            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Rezervasyonlar - {encodedTourTitle}</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; color: #333; }}
        h1 {{ color: #6366f1; border-bottom: 3px solid #22d3ee; padding-bottom: 10px; }}
        .meta {{ margin-bottom: 25px; color: #666; font-size: 14px; }}
        table {{ width: 100%; border-collapse: collapse; margin-top: 15px; }}
        table th, table td {{ padding: 10px; text-align: left; border: 1px solid #ddd; }}
        table th {{ background-color: #6366f1; color: white; }}
        table tr:nth-child(even) {{ background-color: #f9f9f9; }}
        .empty {{ padding: 30px; text-align: center; color: #999; }}
        .status-ok {{ color: #16a34a; font-weight: bold; }}
        .status-pending {{ color: #ca8a04; font-weight: bold; }}
        .status-reject {{ color: #dc2626; font-weight: bold; }}
    </style>
</head>
<body>
    <h1>{encodedTourTitle} - Rezervasyon Yapan Müşteriler</h1>
    <div class='meta'>Toplam: {bookings.Count} rezervasyon | Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm}</div>";

            if (bookings.Any())
            {
                html += @"
    <table>
        <thead>
            <tr>
                <th>Müşteri Adı</th>
                <th>E-posta</th>
                <th>Telefon</th>
                <th>Tarih Aralığı</th>
                <th>Kişi</th>
                <th>Toplam Fiyat</th>
                <th>Durum</th>
                <th>Rezervasyon Tarihi</th>
            </tr>
        </thead>
        <tbody>";
                foreach (var item in bookings.OrderByDescending(x => x.BookingDate))
                {
                    var status = item.Status ?? "Pending";
                    var statusText = status switch
                    {
                        "Approved" => "Onaylandı",
                        "Rejected" => "Reddedildi",
                        _ => "Beklemede"
                    };
                    var statusClass = status switch { "Approved" => "status-ok", "Rejected" => "status-reject", _ => "status-pending" };

                    html += $@"
            <tr>
                <td>{WebUtility.HtmlEncode($"{item.FirstName} {item.LastName}")}</td>
                <td>{WebUtility.HtmlEncode(item.Email ?? "")}</td>
                <td>{WebUtility.HtmlEncode(item.Phone ?? "")}</td>
                <td>{item.CheckInDate:dd.MM.yyyy} - {item.CheckOutDate:dd.MM.yyyy}</td>
                <td>{item.NumberOfPeople}</td>
                <td>{item.TotalPrice:N2} ₺</td>
                <td class='{statusClass}'>{statusText}</td>
                <td>{item.BookingDate:dd.MM.yyyy HH:mm}</td>
            </tr>";
                }
                html += @"
        </tbody>
    </table>";
            }
            else
            {
                html += "<div class='empty'>Bu tura henüz rezervasyon yapılmamış.</div>";
            }

            html += @"
</body>
</html>";

            var pdf = new HtmlToPdf();
            var pdfDoc = pdf.ConvertHtmlString(html);
            var pdfBytes = pdfDoc.Save();
            pdfDoc.Close();

            var fileName = $"Rezervasyonlar_{safeFileName}_{DateTime.Now:yyyyMMdd}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
