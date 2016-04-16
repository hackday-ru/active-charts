using System;
using System.Configuration;
using System.Threading;
using Models;
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
            var dataCollection = db.GetCollection<ChartData>("chartdata");
            var dataTracker = new DataTracker();
            while (true)
            {
                var observes = observeCollection.FindSync(observe => true).ToList();
                foreach (var observe in observes)
                {
                    try
                    {
                        var data = dataTracker.GetDataByXPath(observe.Url, observe.XPath);
                        var r = dataCollection.FindSync(d => d.ObserveId == observe.ObserveId).FirstOrDefault();
                        if (r == null)
                        {
                            dataCollection.InsertOne(new ChartData
                            {
                                ObserveId = observe.ObserveId,
                                Data = "Date Value" + Environment.NewLine + DateTime.Now.ToString() + " " + data,
                                UserId = observe.UserId,
								DateTime = DateTime.Now
                            });
                        }
                        else
                        {
                            r.Data = r.Data + Environment.NewLine + DateTime.Now.ToString() + " " + data;
                            dataCollection.ReplaceOne(o => o.ObservedDataId == r.ObservedDataId, r);
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
