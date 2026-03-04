using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.TestimonialDtos;
using Tripify.WebApi.Services.TestimonialServices;

namespace Tripify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialsController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTestimonials()
        {
            var values = await _testimonialService.GetAllTestimonialAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestimonialById(string id)
        {
            var value = await _testimonialService.GetTestimonialByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            await _testimonialService.CreateTestimonialAsync(createTestimonialDto);
            return Ok("Testimonial created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            await _testimonialService.UpdateTestimonialAsync(updateTestimonialDto);
            return Ok("Testimonial updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(string id)
        {
            await _testimonialService.DeleteTestimonialAsync(id);
            return Ok("Testimonial deleted successfully");
        }
    }
}
