using AutoMapper;
using MongoDB.Driver;
using Tripify.DTOs.GuideDtos;
using Tripify.WebApi.Entities;
using Tripify.WebAPI.Settings;

namespace Tripify.WebApi.Services.GuideServices
{
    public class GuideService : IGuideService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Guide> _guideCollection;
        public GuideService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _guideCollection = database.GetCollection<Guide>(_databaseSettings.GuideCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultGuideDto>> GetAllGuideAsync()
        {
            var guides = await _guideCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultGuideDto>>(guides);
        }

        public async Task CreateGuideAsync(CreateGuideDto createGuideDto)
        {
            var guide = _mapper.Map<Guide>(createGuideDto);
            await _guideCollection.InsertOneAsync(guide);
        }

        public async Task UpdateGuideAsync(UpdateGuideDto updateGuideDto)
        {
            var guide = _mapper.Map<Guide>(updateGuideDto);
            await _guideCollection.FindOneAndReplaceAsync(x => x.GuideId == updateGuideDto.GuideId, guide);
        }

        public async Task DeleteGuideAsync(string id)
        {
            await _guideCollection.DeleteOneAsync(x => x.GuideId == id);
        }

        public async Task<GetGuideByIdDto> GetGuideByIdAsync(string id)
        {
            var guide = await _guideCollection.Find(x => x.GuideId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetGuideByIdDto>(guide);
        }
    }
}
