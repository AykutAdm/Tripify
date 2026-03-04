using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tripify.WebUI.Dtos.TourDtos;

namespace Tripify.WebUI.ViewComponents.TourDetailViewComponents
{
    public class _TourDetailComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TourDetailComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Tours/{tourId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetTourByIdDto>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
