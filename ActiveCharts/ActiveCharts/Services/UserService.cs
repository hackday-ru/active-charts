using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCharts.Models;
using ActiveCharts.Services.Interfaces;

namespace ActiveCharts.Services
{
    public class UserService : IUserService
    {
        public User GetUser(string nickname)
        {
            return new User();
        }
    }
}