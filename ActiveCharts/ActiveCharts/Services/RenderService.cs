using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Models;
using MongoDB.Driver;
using NReco.ImageGenerator;

namespace ActiveCharts.Services
{
    public interface IRenderService
    {
        byte[] GetPngData(string chartId, string chartUrl);
    }

    public class RenderService : IRenderService
    {
        private readonly IMongoDatabase db;

        private const string DbName = "activeCharts";

        public RenderService()
        {
            var client = new MongoClient(new MongoUrl(ConfigurationManager.AppSettings["MongoUrl"]));
            db = client.GetDatabase(DbName);
        }

        public byte[] GetPngData(string chartId, string chartUrl)
        {
            var collection = db.GetCollection<Pngs>("pngs");
            var png = collection.Find(p => p.ChartId == chartId).FirstOrDefault();
            if (png != null)
            {
                return png.Data;
            }
            else
            {
                var htmlToImageConv = new HtmlToImageConverter();
                var memoryStream = new MemoryStream();
                htmlToImageConv.ExecutionTimeout = TimeSpan.FromSeconds(10);
                htmlToImageConv.GenerateImageFromFile(chartUrl, "png", memoryStream);
                htmlToImageConv.GenerateImageFromFile(chartUrl, null, @"resss.png");
                var res = memoryStream.ToArray();
                var p = new Pngs
                {
                    ChartId = chartId,
                    Data = res
                };
                collection.InsertOne(p);
                return res;
            }
        }
    }
}