using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> users;
        static MongoUserRepository()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.MapMember(user => user.Name);
                map.MapMember(user => user.Id);
                map.MapMember(user => user._learned);
                map.MapMember(user => user._inProgress);

            });
        }
        public MongoUserRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("LOD");
            users = database.GetCollection<User>("MapkUsers");
        }

        public User LoadUser(Guid id)
        {
            return users.Find(user => user.Id == id).FirstOrDefault();
        }

        public void SaveUser(User user)
        {
            users.InsertOne(user);
        }
    }
}
