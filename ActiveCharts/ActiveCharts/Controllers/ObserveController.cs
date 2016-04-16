using System.Web.Helpers;
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
            if (!string.IsNullOrEmpty(user))
                user = "qwerty";
            // TODO get user by tokens
            observeService.Add(url, xpath, user);

            return Json(new {IsSuccess = "true"} , JsonRequestBehavior.AllowGet);
        }
    }
}