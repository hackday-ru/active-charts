using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataTracker
{
    [BsonIgnoreExtraElements]
    public class ObservedData
    {
        public string Id { get; set; }
        public string ObserveId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
