using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tripify.WebUI.Dtos.TourDtos;

namespace Tripify.WebUI.ViewComponents.TourViewComponents
{
    public class _TourListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TourListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            int pageSize = 3;
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Tours");
            
            List<ResultTourDto> allValues = new List<ResultTourDto>();
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                allValues = JsonConvert.DeserializeObject<List<ResultTourDto>>(jsonData) ?? new List<ResultTourDto>();
            }

            var totalCount = allValues.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedValues = allValues
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedValues);
        }
    }
}
