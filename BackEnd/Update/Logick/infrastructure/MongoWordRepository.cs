using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.infrastructure
{
    public class MongoWordRepository : IRepository<IPareOfWords>
    {
        private IMongoCollection<PareOfWords> words;

        static MongoWordRepository()
        {
            BsonClassMap.RegisterClassMap<PareOfWords>(map =>
            {
                map.AutoMap();
                map.MapIdMember(word => word.Id);
                map.MapProperty(word => word.EnWord);
                map.MapProperty(word => word.TransWord);
                map.MapCreator(word => new PareOfWords(word.Id, word.EnWord, word.TransWord));
            });
        }

        public MongoWordRepository(string connectionString)
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            IMongoDatabase dateBase = mongoClient.GetDatabase("KolyDB");
            words = dateBase.GetCollection<PareOfWords>("Words");
        }

        public IEnumerable<IPareOfWords> GetAll()
        {
            return words
                .Find(el => true)
                .ToEnumerable();
        }

        public IPareOfWords Load(Guid elementID)
        {
            return words.Find(el => el.Id == elementID).FirstOrDefault();
        }

        public void Save(IPareOfWords element)
        {
            words.InsertOne((PareOfWords)element);
        }
    }
}
