using Tripify.Dtos.TourDtos;

namespace Tripify.Services.TourServices
{
    public class TourManager : ITourService
    {
        public Task CreateTourAsync(CreateTourDto createTourDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTourAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultTourDto>> GetAllTourAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            throw new NotImplementedException();
        }
    }
}
