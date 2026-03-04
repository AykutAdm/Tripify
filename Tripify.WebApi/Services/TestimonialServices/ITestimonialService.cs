using Tripify.DTOs.TestimonialDtos;

namespace Tripify.WebApi.Services.TestimonialServices
{
    public interface ITestimonialService
    {
        Task<List<ResultTestimonialDto>> GetAllTestimonialAsync();
        Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto);
        Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto);
        Task DeleteTestimonialAsync(string id);
        Task<GetTestimonialByIdDto> GetTestimonialByIdAsync(string id);
    }
}
