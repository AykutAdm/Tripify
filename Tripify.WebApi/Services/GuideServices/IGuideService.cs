using Tripify.DTOs.GuideDtos;

namespace Tripify.WebApi.Services.GuideServices
{
    public interface IGuideService
    {
        Task<List<ResultGuideDto>> GetAllGuideAsync();
        Task CreateGuideAsync(CreateGuideDto createGuideDto);
        Task UpdateGuideAsync(UpdateGuideDto updateGuideDto);
        Task DeleteGuideAsync(string id);
        Task<GetGuideByIdDto> GetGuideByIdAsync(string id);
    }
}
