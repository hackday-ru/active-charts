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
	        if (!string.IsNullOrEmpty(token))
	        {
				var nickname = userService.GetNicknameByToken(token);
				observeService.Add(url, xpath, nickname);
                return Json(new { IsSuccess = "true" }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = 403;
            return Json(new { IsSuccess = "false" }, JsonRequestBehavior.AllowGet);

        }
    }
}