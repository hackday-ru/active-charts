using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ActiveCharts.Models;
using ActiveCharts.Services.Interfaces;
using Models;
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

        public string GetObservedData(string id)
        {
            var collection = db.GetCollection<ChartData>("chartdata");
            var r = collection.FindSync(o => o.ObservedDataId == id).FirstOrDefault();
            if (r != null)
            {
                return r.Data;
            }
            return "";
        }

	    public void SaveChart(string data, string currentUser)
	    {
			var collection = db.GetCollection<ChartData>("chartdata");
			collection.InsertOne(new ChartData
			{
				ObservedDataId = Guid.NewGuid().ToString(),
				Data = data,
				UserId = currentUser
			});
	    }
    }
}