﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCharts.Models;

namespace ActiveCharts.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(string nickname);
    }
}
