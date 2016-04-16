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
            var user = usersCollection.Find(node => node.Nickname == nickname).FirstOrDefault();

            return user;
        }

        public bool Login(string nickname, string password)
        {
            var user = GetUser(nickname);

            if (user == null)
            {
                var usersCollection = db.GetCollection<User>("users");

                usersCollection.InsertOneAsync(new User
                {
                    Nickname = nickname,
                    Password = password
                }).GetAwaiter().GetResult();

                return true;
            }

            if (user.Password == password)
            {
                return true;
            }

            return false;
        }
    }
}