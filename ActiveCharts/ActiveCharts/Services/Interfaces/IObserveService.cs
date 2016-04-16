namespace ActiveCharts.Services.Interfaces
{
    public interface IObserveService
    {
        void Add(string url, string xpath, string userId);
        string GetObservedData(string id);
    }
}