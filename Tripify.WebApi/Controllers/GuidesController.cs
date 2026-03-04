using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.GuideDtos;
using Tripify.WebApi.Services.GuideServices;

namespace Tripify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IGuideService _guideService;

        public GuidesController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuides()
        {
            var values = await _guideService.GetAllGuideAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuideById(string id)
        {
            var value = await _guideService.GetGuideByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetGuidesByTourId(string tourId)
        {
            var guides = await _guideService.GetAllGuideAsync();
            var values = guides.Where(x => x.TourId == tourId).ToList();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuide(CreateGuideDto createGuideDto)
        {
            await _guideService.CreateGuideAsync(createGuideDto);
            return Ok("Guide created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuide(UpdateGuideDto updateGuideDto)
        {
            await _guideService.UpdateGuideAsync(updateGuideDto);
            return Ok("Guide updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuide(string id)
        {
            await _guideService.DeleteGuideAsync(id);
            return Ok("Guide deleted successfully");
        }
    }
}
