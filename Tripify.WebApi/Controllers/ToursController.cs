using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.TourDtos;
using Tripify.WebAPI.Services.TourServices;

namespace Tripify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
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
    }
}
