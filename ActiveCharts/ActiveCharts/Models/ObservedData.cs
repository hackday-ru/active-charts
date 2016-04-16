using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActiveCharts.Models
{
    [BsonIgnoreExtraElements]
    public class ObservedData
    {
        public ObjectId _id { get; set; }
        public ObjectId ObserveId { get; set; }
        public string ChartData { get; set; }
        //public DateTime Date { get; set; }
        //public decimal Value { get; set; }
    }
}