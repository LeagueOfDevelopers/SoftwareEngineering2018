using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.infrastructure
{
    public class MongoUserRepository : IRepository<IUser>
    {
        private IMongoCollection<User> users;

        static MongoUserRepository()
        {
            BsonClassMap.RegisterClassMap<UserPareOfWords>(map =>
            {
                map.AutoMap();  
                map.MapIdMember(word => word.Id);
                map.MapMember(word => word.EnWord);
                map.MapMember(word => word.TransWord);
                map.MapMember(word => word.DepthOfLearning);
                map.MapCreator(word => new UserPareOfWords(word.Id, word.EnWord, word.TransWord, word.DepthOfLearning));
            });
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.MapIdMember(user => user.Id);
                map.MapMember(user => user.Name);
                map.MapMember(user => user.Words);
                map.MapCreator(user => new User(user.Id,
                    user.Name,
                    user.Words));
            });

        }

        public MongoUserRepository(string connectionString)
        {
            MongoClient mongoClien = new MongoClient(connectionString);
            IMongoDatabase database = mongoClien.GetDatabase("KolyDB"); //бд
            users = database.GetCollection<User>("Users"); //коллекция
        }

        public IEnumerable<IUser> GetAll()
        {
            return users.Find(el => true).ToList();
        }

        public IUser Load(Guid elementID)
        {
            return users.Find(user => user.Id == elementID).FirstOrDefault();
        }

        public void Save(IUser element)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("_id", element.Id);
            users.ReplaceOne(filter, (User)element);
            
        }
    }
}
