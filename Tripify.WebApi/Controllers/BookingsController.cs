using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.BookingDtos;
using Tripify.WebApi.Services.BookingServices;
using Tripify.WebApi.Services.OpenAIServices;
using Tripify.WebAPI.Services.TourServices;

namespace Tripify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IOpenAIService _openAIService;
        private readonly ITourService _tourService;

        public BookingsController(IBookingService bookingService, IOpenAIService openAIService, ITourService tourService)
        {
            _bookingService = bookingService;
            _openAIService = openAIService;
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var values = await _bookingService.GetAllBookingAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(string id)
        {
            var value = await _bookingService.GetBookingByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetBookingsByTourId(string tourId)
        {
            var bookings = await _bookingService.GetBookingsByTourIdAsync(tourId);
            return Ok(bookings);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetBookingsByEmail(string email)
        {
            var bookings = await _bookingService.GetBookingsByEmailAsync(email);
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            await _bookingService.CreateBookingAsync(createBookingDto);
            return Ok("Booking created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            await _bookingService.UpdateBookingAsync(updateBookingDto);
            return Ok("Booking updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return Ok("Booking deleted successfully");
        }

        [HttpGet("{id}/status-email")]
        public async Task<IActionResult> GetStatusEmail(string id, [FromQuery] string status)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            string? tourTitle = null;
            if (!string.IsNullOrWhiteSpace(booking.TourId))
            {
                var tour = await _tourService.GetTourByIdAsync(booking.TourId);
                tourTitle = tour?.Title;
            }

            var emailBody = await _openAIService.GenerateBookingStatusEmailAsync(booking, status, tourTitle);

            return Ok(new
            {
                subject = $"Tripify Rezervasyon Durumunuz: {status}",
                body = emailBody,
                receiverEmail = booking.Email
            });
        }
    }
}
