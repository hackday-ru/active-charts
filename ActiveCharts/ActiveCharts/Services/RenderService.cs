using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Models;
using MongoDB.Driver;
using NReco.ImageGenerator;
using NReco.PhantomJS;

namespace ActiveCharts.Services
{
    public interface IRenderService
    {
        byte[] GetPngData(string chartId, string chartUrl, string filePath);
        byte[] GetSvgData(string id, string action, string path);
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

        public byte[] GetPngData(string chartId, string chartUrl, string filePath)
        {
            var collection = db.GetCollection<Pngs>("pngs");
            var png = collection.Find(p => p.ChartId == chartId).FirstOrDefault();
            if (png != null)
            {
                return png.Data;
            }
            else
            {
                var phantomJS = new PhantomJS();
                var stream = new MemoryStream( );
                phantomJS.Run(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rasterize.js"),
                        new[] { chartUrl, filePath }, null, stream);

                var res = File.ReadAllBytes(filePath);
                var p = new Pngs
                {
                    ChartId = chartId,
                    Data = res
                };
                collection.InsertOne(p);
                return res;
            }
        }

        public byte[] GetSvgData(string chartId, string chartUrl, string filePath)
        {
            var collection = db.GetCollection<Svgs>("svgs");
            var svg = collection.Find(p => p.ChartId == chartId).FirstOrDefault();
            if (svg != null)
            {
                return svg.Data;
            }
            else
            {
                var phantomJS = new PhantomJS();
                var stream = new MemoryStream();
                phantomJS.Run(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "render-google-chart.js"),
                    new[] {chartUrl, filePath }, null, stream);

                var res = File.ReadAllBytes(filePath);
                var p = new Svgs
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