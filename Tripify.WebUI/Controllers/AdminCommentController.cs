using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripify.WebUI.Dtos.CommentDtos;

namespace Tripify.WebUI.Controllers
{
    public class AdminCommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CommentList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Comments");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> RemoveComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7250/api/Comments/{id}");
            return RedirectToAction("CommentList");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id, bool isActive)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Comments/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CommentList");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var comment = JsonConvert.DeserializeObject<UpdateCommentDto>(jsonData);
            if (comment == null)
            {
                return RedirectToAction("CommentList");
            }

            comment.IsStatus = isActive;

            var updateJson = JsonConvert.SerializeObject(comment);
            StringContent stringContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7250/api/Comments", stringContent);

            return RedirectToAction("CommentList");
        }
    }
}

