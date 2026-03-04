using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tripify.WebApi.Entities
{
    public class Guide
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GuideId { get; set; }
        public string GuideNameSurname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string TourId { get; set; }
    }
}
