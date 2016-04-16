using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ActiveCharts.Models;
using ActiveCharts.Services.Interfaces;
using MongoDB.Driver;

namespace ActiveCharts.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoDatabase db;

        private const string DbName = "activeCharts";

        public UserService()
        {
            var client = new MongoClient(new MongoUrl(ConfigurationManager.AppSettings["MongoUrl"]));
            db = client.GetDatabase(DbName);
        }

        public User GetUser(string nickname)
        {
            var usersCollection = db.GetCollection<User>("users");
            var user = usersCollection.Find(node => node.Nickname.ToLower() == nickname.ToLower()).FirstOrDefault();

            return user;
        }
    }
}