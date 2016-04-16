using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Controllers
{
    public class BaseController : Controller
    {
        public IAuthService AuthService { get; set; }
        public IUserService UserService { get; set; }

        public string CurrentUser
        {
            get { return AuthService.GetUser(HttpContext); }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            try
            {
                base.Initialize(requestContext);
                if (CurrentUser == null) return;
                var user = UserService.GetUser(CurrentUser);
                ViewBag.CurrentUser = user;
            }
            catch (Exception ex)
            {
                AuthService.LogoutUser(requestContext.HttpContext);
                ViewBag.CurrentUser = null;
            }

        }
    }
}