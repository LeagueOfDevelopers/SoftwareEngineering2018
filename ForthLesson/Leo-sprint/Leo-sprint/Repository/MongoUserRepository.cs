using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leo_sprint
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserPub> users;

        public MongoUserRepository(string mongoConnection)
        {
            var mongoClient = new MongoClient(mongoConnection);
            var DateBase = mongoClient.GetDatabase("TanyaDatabase");
            users = DateBase.GetCollection<UserPub>("UserPub");
        }
        static MongoUserRepository()
        {
            BsonClassMap.RegisterClassMap<UserPub>(map =>
            {
                map.AutoMap();
                map.MapIdMember(user => user._id);
                map.MapMember(user => user._nickname);
                map.MapMember(user => user.learned_words);
                map.MapMember(user => user.words_in_process);
            });

        }

        public User LaodUser(Guid id)
        {
            try { return users.Find(user => user._id == id).First().ToUser(); }
            catch { return new User(string.Empty, Guid.Empty, new List<Word>(), new List<Word>()); }
        }
        public Guid CreateUser(string nickname)
        {
            var id = Guid.NewGuid();
            var user = new UserPub(nickname, id, new List<string>(), new List<string>());
            users.InsertOne(user);
            return id;
        }


        public void SaveUser(User user)
        {
            users.ReplaceOne(m => user._id == m._id, user.ToUserPub());
        }
    }

}
