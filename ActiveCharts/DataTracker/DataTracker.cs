using System;
using System.Net;

namespace DataTracker
{
    public interface IDataTracker
    {
        decimal GetDataByUrl(string url);
        decimal GetDataByXPath(string url, string xpath);
    }


    public class DataTracker : IDataTracker
    {
        public decimal GetDataByUrl(string url)
        {
            var response = new WebClient().DownloadString("url");
            var data = MineData(response);
            return data.Value;
        }

        public decimal GetDataByXPath(string url, string xpath)
        {
            return 1.0m;
            var tracker = new SeleniumTracker.SeleniumTracker();
            var element = tracker.GetDataByXPath(url, xpath);
            var data = MineData(element);
            return data.Value;
        }

        private decimal? MineData(object data)
        {
            int? minedData = null;
            if (data is string)
            {
                var dataText = data as string;
                minedData = int.Parse(dataText);
                return minedData;
            }

            return null;
        }

    }
}
