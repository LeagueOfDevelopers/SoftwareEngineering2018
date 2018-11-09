using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WordContext
{
    public class MongoWordsRepository : IWordsRepository
    {
        private readonly IMongoCollection<WordPair> _words;

        static MongoWordsRepository()
        {
            BsonClassMap.RegisterClassMap<WordPair>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(wordPair => wordPair.Origin);
                map.MapMember(wordPair => wordPair.Translation);
            });
        }

        public MongoWordsRepository(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("WordDB");
            _words = database.GetCollection<WordPair>("Words");
        }

        public IEnumerable<WordPair> GetAllWords()
        {
            return _words.AsQueryable();
        }

        public IEnumerable<WordPair> GetRandomPairWords(int amount)
        {
            Random r = new Random();

            return _words.AsQueryable().ToList().OrderBy(wordPair => r.Next()).Take(amount);
        }

        public void AddWordPairs(IEnumerable<WordPair> wordsWithTranslations)
        {
            _words.InsertMany(wordsWithTranslations);
        }

        public void AddWordPair(WordPair wordPair)
        {
            _words.InsertOne(wordPair);
        }

        public IEnumerable<WordPair> GetUnlearnedWords(IEnumerable<WordPair> learnedWords)
        {
            return _words.AsQueryable().ToList().Except(learnedWords);
        }

        public void RemovePair(WordPair wordPair)
        {
            throw new System.NotImplementedException();
        }
    }
}