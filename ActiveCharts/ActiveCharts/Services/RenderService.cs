using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using Models;
using MongoDB.Driver;

namespace ActiveCharts.Services
{
    public interface IRenderService
    {
        byte[] GetPngData(string chartId);
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

        public byte[] GetPngData(string chartId)
        {
            var collection = db.GetCollection<Pngs>("pngs");
            var png = collection.Find(p => p.ChartId == chartId).FirstOrDefault();
            if (png != null)
            {
                return png.Data;
            }
            else
            {
                
            }
            throw new System.NotImplementedException();
        }
    }
}