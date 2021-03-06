﻿using System;
using System.Configuration;
using System.Text.RegularExpressions;
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
            var interval = int.Parse(ConfigurationManager.AppSettings["Interval"]);
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
                                ObservedDataId = Guid.NewGuid().ToString(),
                                ObserveId = observe.ObserveId,
                                Data = "Date Value" + "\n" + DateTime.Now.ToString("_dd/MM/yy_hh:mm:ss") + " " + data,
                                UserId = observe.UserId,
								DateTime = DateTime.Now
                            });
                        }
                        else
                        {
                            r.Data = r.Data + "\n" + DateTime.Now.ToString("_dd/MM/yy_hh:mm:ss") + " " + data;
                            dataCollection.ReplaceOne(o => o.ObservedDataId == r.ObservedDataId, r);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Thread.Sleep(interval);
            }
        }
    }
}
