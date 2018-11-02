using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WordGame.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OrderRandomWord_GetRandomWord()
        {
            DictionaryRepository dictionary = new DictionaryRepository();
            dictionary.SaveWord("Pear", "Груша");
            dictionary.SaveWord("Orange", "Апельсин");
            WordForGame randomWord = dictionary.GetRandomWord();

            Assert.IsNotNull(randomWord);
        }
        [TestMethod]
        public void SaveNewWord_GetThisWordInDictionary()
        {
            DictionaryRepository dictionary = new DictionaryRepository();
            dictionary.SaveWord("Cherry", "Вишня");
            string[] Words = File.ReadAllLines("Dictionary.json");
            List<Word> AllWords = Words
                .Select(serializedWord => JsonConvert.DeserializeObject<Word>(serializedWord))
                .ToList();
            string excpectedWord = "Cherry";
            string currentWord = AllWords.Last().CurrentWord;

            Assert.AreEqual(excpectedWord, currentWord);
        }
    }
} 