using System;
using System.Configuration;
using System.Threading;
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
                        var r = dataCollection.FindSync(d => d.ObserveId == observe._id).FirstOrDefault();
                        if (r == null)
                        {
                            dataCollection.InsertOne(new ObservedData
                            {
                                ObserveId = observe._id,
                                ChartData = "Date Value" + Environment.NewLine + DateTime.Now.ToString() + " " + data
                            });
                        }
                        else
                        {
                            r.ChartData = r.ChartData + Environment.NewLine + DateTime.Now.ToString() + " " + data;
                            dataCollection.ReplaceOne(o => o._id == r._id, r);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
