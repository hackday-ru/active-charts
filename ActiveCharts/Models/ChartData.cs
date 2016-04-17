using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    [BsonIgnoreExtraElements]
    public class ChartData
    {
        public string ObservedDataId { get; set; }
        public string ObserveId { get; set; }
        public string Data { get; set; }
        public string UserId { get; set; }
		public DateTime DateTime { get; set; }
        public bool? ByUrl { get; set; }
        public string Url { get; set; }
    }
}
