using System;
using System.Configuration;
using MongoDB.Driver;

namespace DataTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient(new MongoUrl(ConfigurationManager.AppSettings["MongoUrl"]));
            string DbName = "activeCharts";
            var db = client.GetDatabase(DbName);
            var observeCollection = db.GetCollection<Observe>("observe");
            var dataCollection = db.GetCollection<ObservedData>("observeddata");
            var dataTracker = new DataTracker();
            while (true)
            {
                var observes = observeCollection.FindSync(observe => true).ToList();
                foreach (var observe in observes)
                {
                    try
                    {
                        var data = dataTracker.GetDataByXPath(observe.Url, observe.XPath);
                        dataCollection.InsertOne(new ObservedData
                        {
                            Date = DateTime.Now,
                            ObserveId = observe.Id,
                            Value = data
                        });
                    }
                    catch 
                    {
                        
                    }
                }
            }
            dataTracker.GetDataByXPath("http://www.cbr.ru/", "//*[@id=\"widget_exchange\"]/div/table/tbody/tr[2]/td[2]");
            //"//*[@id="widget_exchange"]/div/table/tbody/tr[2]/td[2]/text()"
            Console.ReadLine();
        }
    }
}
