using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
                ObserveId = Guid.NewGuid().ToString(),
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
                if (r.ByUrl.HasValue && r.ByUrl.Value)
                {
                    var response = new WebClient().DownloadString(r.Url + "?" + Guid.NewGuid());
                    return response;
                }
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
				UserId = currentUser,
				DateTime = DateTime.Now
			});
	    }

        public void AddChartWithUrl(string url, string user)
        {
            var response = new WebClient().DownloadString(url);
            var collection = db.GetCollection<ChartData>("chartdata");
            collection.InsertOne(new ChartData
            {
                ObservedDataId = Guid.NewGuid().ToString(),
                Data = response,
                UserId = user,
                DateTime = DateTime.Now,
                ByUrl = true,
                Url = url
            });

        }

        public List<ChartData> GetUserCharts(string currentUser)
	    {
			var collection = db.GetCollection<ChartData>("chartdata");
		    return collection.FindSync(c => c.UserId == currentUser).ToList();
	    }

	    public void UpdateChart(string id, string data)
	    {
			var collection = db.GetCollection<ChartData>("chartdata");
			var r = collection.FindSync(d => d.ObservedDataId == id).FirstOrDefault();
		    r.Data = data;

		    collection.ReplaceOne(c => c.ObservedDataId == id, r);

	        try
	        {
                var pngCollection = db.GetCollection<Pngs>("pngs");
	            pngCollection.DeleteOne(p => p.ChartId == id);
	        }
	        catch (Exception)
	        {
	        }
	    }
       
    }
}