using System;
using System.Linq;
using BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TraineeUserTests
    {
        [TestMethod]
        public void SaveWrongAnsweredWordsTest_GiveNewWord_WordAddedInStudiedWords()
        {
            var user = new TraineeUser(
                Guid.NewGuid(),
                "David",
                new Word[] { },
                new StudiedWord[] { });
            var word = new Word("test");
            var studiedWord = new StudiedWord(word.Value, 0);

            user.SaveWrongAnsweredWords(new []{word});

            CollectionAssert.Contains(user.StudiedWords.ToList(), studiedWord);
        }

        [TestMethod]
        public void SaveWrongAnsweredWordsTest_GiveAlreadyStudiedWord_StudiedWordsDoesNotChange()
        {
            var word = new Word("test");
            var studiedWord = new StudiedWord(word.Value, 0);
            var user = new TraineeUser(
                Guid.NewGuid(),
                "David",
                new Word[] { },
                new[] {studiedWord});

            user.SaveWrongAnsweredWords(new[] { word });

            Assert.AreEqual(user.StudiedWords.Count(), 1);
        }

        [TestMethod]
        public void SaveCorrectAnsweredWordsTest_GiveNewWord_WordAddedInStudiedWords()
        {
            var word = new Word("test");
            var studiedWord = new StudiedWord(word.Value, 1);
            var user = new TraineeUser(
                Guid.NewGuid(),
                "David",
                new Word[] { },
                new StudiedWord[] { });

            user.SaveCorrectAnsweredWords(new[] { word });

            CollectionAssert.Contains(user.StudiedWords.ToList(), studiedWord);
        }

        [TestMethod]
        public void SaveCorrectAnsweredWordsTest_GiveAlreadyStudiedWord_WordsCounterIncreased()
        {
            var word = new Word("test");
            var user = new TraineeUser(
                Guid.NewGuid(),
                "David",
                new Word[] { },
                new [] 
                {
                    new StudiedWord(word.Value, 0)
                });
            
            user.SaveCorrectAnsweredWords(new[] { word });

            Assert.AreEqual(user
                .StudiedWords
                .ToList()[0]
                .RightAnswersCount, 1);
        }

        [TestMethod]
        public void SaveCorrectAnsweredWordsTest_GiveStudiedWordWichCounterEqualLearnedCounter_WordAddedInLearnedWord()
        {
            var word = new Word("test");
            var user = new TraineeUser(
                Guid.NewGuid(),
                "David",
                new Word[] { },
                new[]
                {
                    new StudiedWord(word.Value, 2)
                });

            user.SaveCorrectAnsweredWords(new[] {word});

            CollectionAssert.Contains(user.LearnedWords.ToList(), word);
        }


    }
}
