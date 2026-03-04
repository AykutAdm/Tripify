using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tripify.WebUI.Dtos.CommentDtos;

namespace Tripify.WebUI.ViewComponents.TourDetailViewComponents
{
    public class _TourDetailCommentListByTourComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TourDetailCommentListByTourComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Comments/tour/{tourId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var comments = JsonConvert.DeserializeObject<List<ResultCommentListByTourIdDto>>(jsonData);
                return View(comments);
            }

            return View(new List<ResultCommentListByTourIdDto>());
        }
    }
}
