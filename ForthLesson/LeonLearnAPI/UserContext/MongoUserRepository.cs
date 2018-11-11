using System;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace UserContext
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        static MongoUserRepository()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.MapIdField(user => user.Id);
                map.MapField(user => user.Name);;
                map.MapField(user => user.RegisterDate);
            });
        }

        public MongoUserRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("UserDB");
            _users = database.GetCollection<User>("Users");
        }

        public User GetUser(Guid userId)
        {
            if (!IsUser(userId)) throw new AuthenticationException("No such user");

            // todo DEBUG
            var us = _users.Find(user => user.Id == userId);

            var ust = us.FirstOrDefault();

            return ust;
        }

        public void AddUser(User user)
        {
            if (IsUser(user.Id)) throw new InvalidOperationException("User already exists");

            _users.InsertOne(user);
        }

        public void DeleteUser(Guid userId)
        {
            if (!IsUser(userId)) throw new AuthenticationException("No such user");

            _users.DeleteOne(user => user.Id == userId);
        }

        public void EditUser(User editedUser)
        {
            if (!IsUser(editedUser.Id)) throw new AuthenticationException("No such user");

            _users.ReplaceOne(user => user.Id == editedUser.Id, editedUser);
        }

        public bool IsUser(Guid userId)
        {
            var d = _users.CountDocuments(user => user.Id == userId);
            return d > 0;
        }
    }
}