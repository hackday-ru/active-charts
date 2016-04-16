using System.Web.Helpers;
using System.Web.Mvc;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Controllers
{
    public class ObserveController : BaseController
    {
        private readonly IObserveService observeService;
        private readonly IUserService userService;

		public ObserveController(IObserveService observeService, IUserService userService)
        {
            this.observeService = observeService;
			this.userService = userService;
        }
        public JsonResult Add(string url, string xpath, string token)
        {
            var user = CurrentUser;
	        if (!string.IsNullOrEmpty(user))
	        {
				var nickname = userService.GetNicknameByToken(token);
				observeService.Add(url, xpath, nickname);
	        }

            return Json(new {IsSuccess = "true"} , JsonRequestBehavior.AllowGet);
        }
    }
}