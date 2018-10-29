using System;
using System.Collections.Generic;
using System.Linq;
using LeonLearn;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LeonLearnTests
{
    [TestClass]
    public class JsonWordRepositoryTests
    {
        [TestMethod]
        public void GetRandomPair()
        {
            var wordRepo = new JsonWordsRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestWords.json");

            var a = wordRepo.GetRandomPairWords(1).ToArray();

            Assert.IsTrue(a[0] is WordPair);
        }

        [TestMethod]
        public void AddWords()
        {
            var wordRepo = new JsonWordsRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestWords.json");

            var newPair = new WordPair("Hell", "Ад");
            wordRepo.AddWordPair(newPair);
        }

        [TestMethod]
        public void GetUnlearnedWords_()
        {
            var wordRepo =
                new JsonWordsRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestWordsSmall.json");

            var sourceWords = new WordPair[]
            {
                new WordPair("Hell", "Ад"),
                new WordPair("Hi", "Здрасьте"),
            };
            var learnedWords = new List<WordPair> {new WordPair("Hell", "Ад")};
            var expected = new List<WordPair> {new WordPair("Hi", "Здрасьте")};

            var a = wordRepo.GetUnlearnedWords(learnedWords).ToArray();

            CollectionAssert.AreEquivalent(expected, a);
        }

        [TestMethod]
        public void IsInside()
        {
            var wordRepo =
                new JsonWordsRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestWordsSmall.json");
            
            var wordPair = new WordPair("Hell", "Ад");

            var result = wordRepo.IsInside(wordPair);
            
            Assert.IsTrue(result);
        }
    }
}