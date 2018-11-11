using System;
using BusinessEntities;
using Data.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Data.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        static MongoUserRepository()
        {
            BsonClassMap.RegisterClassMap<TraineeUser>(map =>
            {
                map.AutoMap();
                map.MapIdMember(user => user.Id);
                map.MapMember(user => user.NickName);
                map.MapMember(user => user.LearnedWords);
                map.MapMember(user => user.StudiedWords);
            });
        }

        public MongoUserRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var databsase = mongoClient.GetDatabase("DavidLoDSprint");
            _users = databsase.GetCollection<TraineeUser>("Users");
        }

        public TraineeUser LoadUser(Guid id)
        {
            return _users.Find(user => user.Id == id)
                .FirstOrDefault();
        }

        public void SaveUser(TraineeUser traineeUser)
        {
            var containInCollection = _users.Find(user => user.Id == traineeUser.Id)
                .FirstOrDefault() != null;
            if (containInCollection)
                _users.DeleteOne(user => user.Id == traineeUser.Id);
            _users.InsertOne(traineeUser);
        }

        private readonly IMongoCollection<TraineeUser> _users;
    }
}
