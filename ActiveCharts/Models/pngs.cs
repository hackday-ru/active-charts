using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    [BsonIgnoreExtraElements]
    public class Pngs
    {
        public string ChartId { get; set; }
        public byte[] Data { get; set; }
    }
}
