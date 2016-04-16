using System.Web.Mvc;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Controllers
{
    public class ObserveController : BaseController
    {
        private readonly IObserveService observeService;

        public ObserveController(IObserveService observeService)
        {
            this.observeService = observeService;
        }
        public JsonResult Add(string url, string xpath, string token)
        {
            var user = CurrentUser;
            // TODO get user by tokens
            observeService.Add(url, xpath, user);
            return null;
        }
    }
}