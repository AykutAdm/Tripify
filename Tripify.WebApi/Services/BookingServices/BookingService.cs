using AutoMapper;
using MongoDB.Driver;
using Tripify.DTOs.BookingDtos;
using Tripify.WebApi.Entities;
using Tripify.WebAPI.Settings;

namespace Tripify.WebApi.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Booking> _bookingCollection;

        public BookingService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(_databaseSettings.BookingCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultBookingDto>> GetAllBookingAsync()
        {
            var bookings = await _bookingCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(bookings);
        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var booking = _mapper.Map<Booking>(createBookingDto);
            await _bookingCollection.InsertOneAsync(booking);
        }

        public async Task UpdateBookingAsync(UpdateBookingDto updateBookingDto)
        {
            var booking = _mapper.Map<Booking>(updateBookingDto);
            await _bookingCollection.FindOneAndReplaceAsync(x => x.BookingId == updateBookingDto.BookingId, booking);
        }

        public async Task DeleteBookingAsync(string id)
        {
            await _bookingCollection.DeleteOneAsync(x => x.BookingId == id);
        }

        public async Task<GetBookingByIdDto> GetBookingByIdAsync(string id)
        {
            var booking = await _bookingCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetBookingByIdDto>(booking);
        }

        public async Task<List<ResultBookingDto>> GetBookingsByTourIdAsync(string tourId)
        {
            var bookings = await _bookingCollection.Find(x => x.TourId == tourId).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(bookings);
        }

        public async Task<List<ResultBookingDto>> GetBookingsByEmailAsync(string email)
        {
            var bookings = await _bookingCollection.Find(x => x.Email == email).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(bookings);
        }
    }
}
