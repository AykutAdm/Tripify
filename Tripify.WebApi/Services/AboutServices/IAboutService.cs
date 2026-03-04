using Tripify.DTOs.AboutDtos;

namespace Tripify.WebApi.Services.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task DeleteAboutAsync(string id);
        Task<GetAboutByIdDto> GetAboutByIdAsync(string id);
    }
}
