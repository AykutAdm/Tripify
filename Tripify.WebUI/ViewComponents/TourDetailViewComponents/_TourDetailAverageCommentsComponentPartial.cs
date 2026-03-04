using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tripify.WebUI.ViewComponents.TourDetailViewComponents
{
    public class _TourDetailAverageCommentsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TourDetailAverageCommentsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                // Yorumları al
                var commentsResponse = await client.GetAsync($"https://localhost:7250/api/Comments/tour/{tourId}");
                
                // AI özetini al
                var summaryResponse = await client.GetAsync($"https://localhost:7250/api/Comments/tour/{tourId}/summary");

                var model = new CommentAverageViewModel();

                if (commentsResponse.IsSuccessStatusCode)
                {
                    var jsonData = await commentsResponse.Content.ReadAsStringAsync();
                    var comments = JsonConvert.DeserializeObject<List<CommentDto>>(jsonData);
                    
                    if (comments != null && comments.Any())
                    {
                        model.TotalComments = comments.Count;
                        model.AverageRating = comments.Average(c => c.Score);
                    }
                }

                if (summaryResponse.IsSuccessStatusCode)
                {
                    var jsonData = await summaryResponse.Content.ReadAsStringAsync();
                    var summaryObj = JsonConvert.DeserializeObject<SummaryResponse>(jsonData);
                    model.AISummary = summaryObj?.Summary ?? "Yorumlar analiz edildi.";
                }

                return View(model);
            }
            catch
            {
                return View(new CommentAverageViewModel());
            }
        }
    }

    public class CommentAverageViewModel
    {
        public double AverageRating { get; set; }
        public int TotalComments { get; set; }
        public string AISummary { get; set; } = "Yorumlar yükleniyor...";
    }

    public class CommentDto
    {
        public int Score { get; set; }
    }

    public class SummaryResponse
    {
        public string Summary { get; set; }
    }
}
