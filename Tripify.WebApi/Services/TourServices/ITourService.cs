using Tripify.DTOs.TourDtos;

namespace Tripify.WebAPI.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllTourAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
        Task<GetTourByIdDto> GetTourByIdAsync(string id);
        Task<List<ResultLast4TourDto>> GetLast4TourAsync();
    }
}
