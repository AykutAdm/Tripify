using Tripify.DTOs.BookingDtos;

namespace Tripify.WebApi.Services.BookingServices
{
    public interface IBookingService
    {
        Task<List<ResultBookingDto>> GetAllBookingAsync();
        Task CreateBookingAsync(CreateBookingDto createBookingDto);
        Task UpdateBookingAsync(UpdateBookingDto updateBookingDto);
        Task DeleteBookingAsync(string id);
        Task<GetBookingByIdDto> GetBookingByIdAsync(string id);
        Task<List<ResultBookingDto>> GetBookingsByTourIdAsync(string tourId);
        Task<List<ResultBookingDto>> GetBookingsByEmailAsync(string email);
    }
}
