using System;
using System.Collections.Generic;
using LeonLearn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonLearnTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EqualLessons_AreEqual()
        {
            List<WordPair> wordPairs = new List<WordPair>()
            {
                new WordPair("Hell", "Ад")
            };
            var lessonOne = new Lesson(wordPairs);

            var lessonTwo = new Lesson(wordPairs);

            var hashOne = lessonOne.GetHashCode();
            var hashTwo = lessonTwo.GetHashCode();

            Assert.AreEqual(hashOne, hashTwo);
        }

        [TestMethod]
        public void PresentIdInside()
        {
            Dictionary<Lesson, int> testLessons = new Dictionary<Lesson, int>();

            List<WordPair> wordPairs = new List<WordPair>()
            {
                new WordPair("Hell", "Ад")
            };

            var lessonOne = new Lesson(wordPairs);
            var lessonTwo = new Lesson(wordPairs);
            
            testLessons.Add(lessonOne, 1);

            var res = testLessons[lessonTwo];
            
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void WordsEqual()
        {
            var a = new WordPair("Hell", "Ад");
            var b = new WordPair("HELL", "АД");

            var res = a.Equals(b);

            Assert.IsTrue(res);
        }
        
        [TestMethod]
        public void PresentIdInside_DiffCapitals()
        {
            Dictionary<Lesson, int> testLessons = new Dictionary<Lesson, int>();

            List<WordPair> wordPairsOne = new List<WordPair>()
            {
                new WordPair("Hell", "Ад")
            };
            
            List<WordPair> wordPairsTwo = new List<WordPair>()
            {
                new WordPair("Hell", "Ад")
            };

            var lessonOne = new Lesson(wordPairsOne);
            var lessonTwo = new Lesson(wordPairsTwo);
            
            testLessons.Add(lessonOne, 1);

            var res = testLessons[lessonTwo];
            
            Assert.AreEqual(1, res);
        }
        
        [TestMethod]
        public void LessonsEqual_DiffCaps()
        {
            Dictionary<Lesson, int> testLessons = new Dictionary<Lesson, int>();

            List<WordPair> wordPairsOne = new List<WordPair>()
            {
                new WordPair("Hell", "Ад")
            };
            
            List<WordPair> wordPairsTwo = new List<WordPair>()
            {
                new WordPair("HELL", "Ад")
            };

            var lessonOne = new Lesson(wordPairsOne);
            var lessonTwo = new Lesson(wordPairsTwo);

            Assert.AreEqual(lessonOne, lessonTwo);
        }
    }
}