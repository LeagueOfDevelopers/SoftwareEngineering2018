using LoDSprintApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoDSprintApi.Repositories
{
    public class InFileDictionaryRepository : IDictionaryRepository
    {
        public InFileDictionaryRepository()
        {
            _filePath = "C:/Users/Давид/source/repos/LoDSprintApi/DictionaryRepository";
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "");
        }

        public WordModel GetRandomWord()
        {
            var dictionary = DeserialiazeDictionary();

            var randomIndex = new Random()
                .Next(dictionary.Count);

            return dictionary
                .ElementAt(randomIndex)
                .Word;

        }

        public TranslationModel GetWordTranslation(WordModel word)
        {
            var dictionary = DeserialiazeDictionary();
            return dictionary
                .Find(pair => 
                    pair.Word == word)
                .Translation;
        }

        public void SaveDictionaryPair(DictionaryPairModel dictionaryPair)
        {
            var dictionary = DeserialiazeDictionary();

            if (dictionary.Contains(dictionaryPair))
                dictionary
                    .Remove(dictionaryPair);

            dictionary.Add(dictionaryPair);
            SaveDictionaryInFile(
                SerializeDictionary(dictionary));
                
        }

        private List<DictionaryPairModel> DeserialiazeDictionary()
        {
            return ReadFile()
                .Select(pair =>
                    JsonConvert
                    .DeserializeObject<DictionaryPairModel>(pair))
                .ToList();
        }

        private string[] SerializeDictionary(List<DictionaryPairModel> dictionaryPairs)
        {
            return dictionaryPairs
                .Select(pair => 
                    JsonConvert
                    .SerializeObject(pair))
                .ToArray();
        }

        private string[] ReadFile()
        {
            return File
                .ReadAllLines(_filePath);
        }

        private void SaveDictionaryInFile(string[] serializedDictionary)
        {
            File
                .WriteAllLines(_filePath, serializedDictionary);
        }

        private readonly string _filePath;
    }
}
