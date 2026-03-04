using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.TourDtos;
using Tripify.WebApi.Services.OpenAIServices;
using Tripify.WebAPI.Services.TourServices;

namespace Tripify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;
        private readonly IOpenAIService _openAIService;

        public ToursController(ITourService tourService, IOpenAIService openAIService)
        {
            _tourService = tourService;
            _openAIService = openAIService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTours()
        {
            var values = await _tourService.GetAllTourAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(string id)
        {
            var value = await _tourService.GetTourByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpGet("{id}/what-to-expect")]
        public async Task<IActionResult> GenerateWhatToExpect(string id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null)
                return NotFound("Tur bulunamadı");

            var location = $"{tour.City}, {tour.Country}";
            var whatToExpect = await _openAIService.GenerateWhatToExpectAsync(
                tour.Title,
                tour.Description,
                location,
                tour.DayNight
            );

            return Ok(new { content = whatToExpect });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            await _tourService.CreateTourAsync(createTourDto);
            return Ok("Tour created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
        {
            await _tourService.UpdateTourAsync(updateTourDto);
            return Ok("Tour updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(string id)
        {
            await _tourService.DeleteTourAsync(id);
            return Ok("Tour deleted successfully");
        }

        [HttpGet("GetLast4Tour")]
        public async Task<IActionResult> GetLast4Tour()
        {
            var values = await _tourService.GetLast4TourAsync();
            return Ok(values);
        }
    }
}
