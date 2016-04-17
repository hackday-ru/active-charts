using System.Collections.Generic;
using Models;

namespace ActiveCharts.Services.Interfaces
{
    public interface IObserveService
    {
        void Add(string url, string xpath, string userId);
        string GetObservedData(string id);
	    void SaveChart(string data, string currentUser);
	    List<ChartData> GetUserCharts(string currentUser);
	    void UpdateChart(string id, string data);
        void AddChartWithUrl(string url, string user);

    }
}