using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    [BsonIgnoreExtraElements]
    public class Observe
    {
        public string ObserveId { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string XPath { get; set; }
    }
}
