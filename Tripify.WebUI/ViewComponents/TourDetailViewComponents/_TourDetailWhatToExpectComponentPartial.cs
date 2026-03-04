using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Tripify.WebUI.ViewComponents.TourDetailViewComponents
{
    public class _TourDetailWhatToExpectComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TourDetailWhatToExpectComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Tours/{tourId}/what-to-expect");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<WhatToExpectResponse>(jsonData);

                if (!string.IsNullOrEmpty(response?.Content))
                {
                    // JSON temizleme: backtick ve "json" kelimesini kaldır
                    var cleanedJson = response.Content
                        .Replace("```json", "")
                        .Replace("```", "")
                        .Trim();

                    var days = JsonConvert.DeserializeObject<List<WhatToExpectDay>>(cleanedJson);
                    return View(days);
                }
            }

            return View(new List<WhatToExpectDay>());
        }
    }

    public class WhatToExpectResponse
    {
        public string Content { get; set; }
    }

    public class WhatToExpectDay
    {
        public int Day { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
