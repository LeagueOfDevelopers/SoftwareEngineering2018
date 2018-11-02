using System;
using System.IO;
using System.Linq;

namespace LoDSprint.Repositories
{
    public class InFileDictionaryRepository : IInFileDictionaryRepository
    {
        public InFileDictionaryRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public Word GetRandomWord()
        {
            var dictionary = ReadFile()
                .DeserializeDictionary();
            var randomIndex = new Random()
                .Next(dictionary.Count);

            return dictionary
                .ElementAt(randomIndex)
                .Key;

        }

        public Translation GetWordTranslation(Word word)
        {
            return ReadFile()
                .DeserializeDictionary()
                [word];
        }

        public void SaveDictionaryPair(Word word, Translation translation)
        {
            var dictionary = ReadFile()
                .DeserializeDictionary();

            dictionary.Add(word, translation);
            SaveDictionaryInFile(
                dictionary
                .SerializeDictionary()
                );
        }

        private string ReadFile()
        {
            return File
                .ReadAllText(_filePath);
        }

        private void SaveDictionaryInFile(string serializedDictionary)
        {
            File
                .WriteAllText(_filePath, serializedDictionary);
        }

        private readonly string _filePath;
    }
}
