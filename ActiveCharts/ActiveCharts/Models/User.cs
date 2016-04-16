using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace ActiveCharts.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string Nickname { get; set; }
        public string Password { get; set; }

		public string Token { get; set; }
    }
}