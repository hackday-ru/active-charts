using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Services
{
    public class AuthService : IAuthService
    {
        private const string CookieName = "__AUTH_COOKIE";
        private string currentUser;

        public string GetUser(HttpContextBase httpContext)
        {
            if (currentUser != null) return currentUser;

            var authCookie = httpContext.Request.Cookies[CookieName];
            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value)) return null;
            try
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket == null) return null;

                currentUser = ticket.Name;

                return currentUser;
            }
            catch (Exception ex)
            {
                LogoutUser(httpContext);
                return null;
            }
        }

        public void LoginUser(HttpContextBase httpContext, string nickname)
        {
            var userExpireDays = DateTime.Now.Add(TimeSpan.FromDays(30));

            var ticket = new FormsAuthenticationTicket(
                1,
                nickname,
                DateTime.Now,
                userExpireDays,
                false,
                nickname);
            try
            {
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                SetValue(CookieName, encryptedTicket, userExpireDays);
                currentUser = nickname;

            }
            catch (Exception ex)
            {
                LogoutUser(httpContext);
            }

        }

        public void LogoutUser(HttpContextBase httpContext)
        {
            currentUser = null;
            SetValue(CookieName, null, DateTime.Now.AddYears(-1000));
            //httpContext.User = null;
        }

        private void SetValue(string cookieName, string cookieObject, DateTime dateStoreTo)
        {
            var cookie = HttpContext.Current.Response.Cookies[cookieName] ?? new HttpCookie(cookieName) {Path = "/"};

            cookie.Value = cookieObject;
            cookie.Expires = dateStoreTo;

            HttpContext.Current.Response.SetCookie(cookie);
        }
    }
}