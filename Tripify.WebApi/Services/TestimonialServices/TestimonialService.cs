using AutoMapper;
using MongoDB.Driver;
using Tripify.DTOs.TestimonialDtos;
using Tripify.WebApi.Entities;
using Tripify.WebAPI.Settings;

namespace Tripify.WebApi.Services.TestimonialServices
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Testimonial> _testimonialCollection;

        public TestimonialService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _testimonialCollection = database.GetCollection<Testimonial>(_databaseSettings.TestimonialCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultTestimonialDto>> GetAllTestimonialAsync()
        {
            var testimonials = await _testimonialCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTestimonialDto>>(testimonials);
        }

        public async Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto)
        {
            var testimonial = _mapper.Map<Testimonial>(createTestimonialDto);
            await _testimonialCollection.InsertOneAsync(testimonial);
        }

        public async Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonial = _mapper.Map<Testimonial>(updateTestimonialDto);
            await _testimonialCollection.FindOneAndReplaceAsync(x => x.TestimonialId == updateTestimonialDto.TestimonialId, testimonial);
        }

        public async Task DeleteTestimonialAsync(string id)
        {
            await _testimonialCollection.DeleteOneAsync(x => x.TestimonialId == id);
        }

        public async Task<GetTestimonialByIdDto> GetTestimonialByIdAsync(string id)
        {
            var testimonial = await _testimonialCollection.Find(x => x.TestimonialId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTestimonialByIdDto>(testimonial);
        }
    }
}
