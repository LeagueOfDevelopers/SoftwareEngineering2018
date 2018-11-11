using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WordContext
{
    public class JsonWordsRepository : IWordsRepository
    {
        public string Path { get; set; }

        public JsonWordsRepository(string path)
        {
            Path = path;
        }

        public static JsonWordsRepository Default =
            new JsonWordsRepository(@"/Users/leon/Projects/LeonLearn/LeonLearn/Words.json");

        public IEnumerable<WordPair> GetAllWords()
        {
            return JArray.Parse(File.ReadAllText(Path)).ToObject<WordPair[]>();
        }

        public IEnumerable<WordPair> GetRandomPairWords(int amt)
        {
            Random r = new Random();

            var allWords = JArray.Parse(File.ReadAllText(Path)).ToObject<List<WordPair>>();

            var randomPairs = allWords.OrderBy(item => r.Next()).Take(amt).ToArray();

            return randomPairs;
        }

        public void AddWordPairs(IEnumerable<WordPair> wordsWithTranslations)
        {
            var allWords = JArray.Parse(File.ReadAllText(Path));
            var newWords = allWords.Concat(JArray.FromObject(wordsWithTranslations));

            File.WriteAllText(Path, JsonConvert.SerializeObject(newWords));
        }

        public void AddWordPair(WordPair wordPair)
        {
            var allWords = JArray.Parse(File.ReadAllText(Path));
            allWords.Add(JToken.FromObject(wordPair));

            File.WriteAllText(Path, JsonConvert.SerializeObject(allWords));
        }

        public IEnumerable<WordPair> GetUnlearnedWords(IEnumerable<WordPair> learnedWords)
        {
            var allWords = JArray.Parse(File.ReadAllText(Path)).ToObject<WordPair[]>();

            return allWords.Except(learnedWords);
        }

        public void RemovePair(WordPair wordPair)
        {
            throw new NotImplementedException();
        }

        public bool IsInside(WordPair wordPair)
        {
            return JArray.Parse(File.ReadAllText(Path)).ToObject<WordPair[]>().Contains(wordPair);
        }
    }
}