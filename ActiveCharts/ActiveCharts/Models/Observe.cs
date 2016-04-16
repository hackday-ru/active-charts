using MongoDB.Bson.Serialization.Attributes;

namespace ActiveCharts.Models
{
    [BsonIgnoreExtraElements]
    public class Observe
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string XPath { get; set; }
    }
}