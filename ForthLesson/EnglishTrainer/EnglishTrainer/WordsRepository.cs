using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace EnglishTrainer
{
    public class WordsRepository : IWordsRepository
    {
        public string _jsonFile { get; set; }
        public WordsRepository(string file)
        {
            _jsonFile = file;
        }

        public List<Word> GetWords()
        {
            var rawFiles = File.ReadAllText(_jsonFile);
            var words = JsonConvert.DeserializeObject<List<Word>>(rawFiles);
            return words;

        }

        public void SaveWords(List<Word> words)
        {
            var serialized = JsonConvert.SerializeObject(words);
            File.WriteAllText(_jsonFile, serialized);
        }

        public void SaveWord(Word word)
        {
            var words = GetWords();
            var newlist = new List<Word>(words){word};
            SaveWords(newlist);
        }

    }
}
