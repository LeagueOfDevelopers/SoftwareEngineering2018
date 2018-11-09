using System;
using System.Collections.Generic;
using BusinessEntities;
using Data.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Data.Repositories
{
    public class MongoDictionaryRepository : IDictionaryRepository
    {
        static MongoDictionaryRepository()
        {
            BsonClassMap.RegisterClassMap<DictionaryPair>(map =>
            {
                map.AutoMap();
                map.MapIdMember(dictionaryPair => dictionaryPair.Id);
                map.MapMember(dictionaryPair => dictionaryPair.Word);
                map.MapMember(dictionaryPair => dictionaryPair.Translation);
            });
        }

        public MongoDictionaryRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var databsase = mongoClient.GetDatabase("DavidLoDSprint");
            _dictionary = databsase.GetCollection<DictionaryPair>("Dictionary");
        }

        public Translation GetWordTranslation(Word word)
        {
            return _dictionary.Find(dictionaryPair => dictionaryPair.Word == word)
                .FirstOrDefault()
                .Translation;
        }

        public Word LoadRandomWord()
        {
            var pairs = _dictionary.Find(pair => true)
                .ToList();
            var randomIndex = new Random().Next(pairs.Count);

            return pairs[randomIndex].Word;
        }

        public void SaveDictionaryPair(DictionaryPair dictionaryPair)
        {
            var equalPairFromDictionary = _dictionary.Find(pair => 
                pair.Word == dictionaryPair.Word)
                .FirstOrDefault();

            if(equalPairFromDictionary == null)
                _dictionary.InsertOne(dictionaryPair);
        }

        public IEnumerable<DictionaryPair> LoadDictionary()
        {
            return _dictionary.Find(pair => true)
                .ToList();
        }

        public void DeleteDictionaryPair(Guid pairId)
        {
            _dictionary
                .DeleteOne(pair => pair.Id == pairId);
        }

        private readonly IMongoCollection<DictionaryPair> _dictionary;
    }
}
