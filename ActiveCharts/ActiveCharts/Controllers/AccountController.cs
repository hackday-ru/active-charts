using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCharts.Services;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nickname, string password)
        {
            var result = userService.Login(nickname, password);

            if (result)
            {
                AuthService.LoginUser(ControllerContext.HttpContext, nickname);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorText = "wrong password";
            return View("Login");
        }

		public ActionResult Logout()
		{
			AuthService.LogoutUser(ControllerContext.HttpContext);

			return RedirectToAction("Index", "Home");
		}

	    public JsonResult GetToken(string nickname, string password)
	    {
		    if (!userService.Login(nickname, password)) return null;

		    var token = userService.CreateToken(nickname);
		    return Json(new { token = token}, JsonRequestBehavior.AllowGet);
	    }
    }
}