using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCharts.Services.Interfaces
{
    public interface IAuthService
    {
        string GetUser(HttpContextBase httpContext);
        void LoginUser(HttpContextBase httpContext, string nickname);
        void LogoutUser(HttpContextBase httpContext);
    }
}