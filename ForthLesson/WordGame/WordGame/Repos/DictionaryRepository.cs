using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace WordGame
{
    public class DictionaryRepository : IDictionaryRepository
    {
        public WordForGame GetRandomWord()
        {
            Random rnd = new Random();
            List<Word> AllWords = DeserializeDictionary();
            Word randomWord = AllWords
                .ElementAt(rnd.Next(0, AllWords.Count()));
            Word secondRandomWord = AllWords
                .ElementAt(rnd.Next(0, AllWords.Count()));
            WordForGame word = new WordForGame(
                randomWord.CurrentWord, 
                randomWord.Translation,
                rnd.Next(0, 1) == 1  ? randomWord.Translation : secondRandomWord.Translation);
            return word;
        }
        public string SerializeWord(string word, string translation)
        {
            Word wordToSave = new Word(word, translation);
            string wordToJson = JsonConvert.SerializeObject(wordToSave);
            return wordToJson;
        }
        public List<Word> DeserializeDictionary()
        {
            string[] Words = File.ReadAllLines(path);
            List<Word> AllWords = Words
                .Select(JsonWord => JsonConvert.DeserializeObject<Word>(JsonWord))
                .ToList();
            return AllWords;
        }
        public void SaveWord(string word, string translation)
        {
            string wordToJson = SerializeWord(word, translation);
            File.AppendAllText(path, wordToJson + '\n');
        }

        private string path = "Dictionary.json";
    }
}
