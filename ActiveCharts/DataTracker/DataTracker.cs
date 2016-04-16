using System;
using System.Net;

namespace DataTracker
{
    public interface IDataTracker
    {
        void GetDataByUrl(string url);
        void GetDataByXPath(string url, string xpath);
    }


    public class DataTracker : IDataTracker
    {
        public void GetDataByUrl(string url)
        {
            var response = new WebClient().DownloadString("url");
            var data = MineData(response);
            HandleData(data);
        }

        public void GetDataByXPath(string url, string xpath)
        {
            var tracker = new SeleniumTracker.SeleniumTracker();
            var element = tracker.GetDataByXPath(url, xpath);
            var data = MineData(element);
            HandleData(data);
        }

        private int? MineData(object data)
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

        private void HandleData(int? data)
        {
            throw new NotImplementedException();
        }
    }
}
