using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ActiveCharts.Models;
using ActiveCharts.Services.Interfaces;
using MongoDB.Driver;

namespace ActiveCharts.Services
{
    public class ObserveService : IObserveService
    {
        private readonly IMongoDatabase db;

        private const string DbName = "activeCharts";

        public ObserveService()
        {
            var client = new MongoClient(new MongoUrl(ConfigurationManager.AppSettings["MongoUrl"]));
            db = client.GetDatabase(DbName);
        }

        public void Add(string url, string xpath, string userId)
        {
            var collection = db.GetCollection<Observe>("observe");
            collection.InsertOne(new Observe
            {
                Url = url,
                UserId = userId,
                XPath = xpath
            });
        }
    }
}