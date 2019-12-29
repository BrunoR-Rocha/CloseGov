using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProjetoDis.Models;

namespace ProjetoDis.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;

        public UserService(IConfiguration config)
        {
            MongoClient client = new MongoClient("mongodb+srv://admin:admin@sinkingshipscluster-kajnp.mongodb.net/test?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("CloseGov");
            users = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {
            return users.Find(user => true).ToList();
        }

        public User Get(int id)
        {
            return users.Find(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            users.InsertOne(user);
            return user;
        }

        public void Update(int id, User userIn)
        {
            users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            users.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(int id)
        {
            users.DeleteOne(user => user.Id == id);
        }

    }

}