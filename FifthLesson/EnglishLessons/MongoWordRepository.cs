using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class MongoWordRepository : IWordRepository
    {
        private readonly IMongoCollection<RepWord> words;
        static MongoWordRepository()
        {
            BsonClassMap.RegisterClassMap<RepWord>(map =>
            {
                map.AutoMap();
                map.MapMember(word => word.Eng);
                map.MapMember(word => word.Rus);
            });
        }
        public MongoWordRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("LOD");
            words = database.GetCollection<RepWord>("MapkWords");
        }
        
        public RepWord LoadWord()
        {
            return words.Find(word => word.Eng != "123").FirstOrDefault();//добавить рандом (?)
        }

        public void SaveWord(RepWord repword)
        {
            words.InsertOne(repword);
        }
    }
}
