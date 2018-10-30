using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnglishLessons
{
    [TestClass]
    public class UserWordTest
    {
        [TestMethod]
        public void AddAnswerTest_0to1()
        {
            UserWord word = new UserWord(false, 0, "eng", "rus");
            UserWord expected = new UserWord(false, 1, "eng", "rus");
            word.Upgrade();

            Assert.ReferenceEquals(word, expected);
        }

        [TestMethod]
        public void CheckSameTest()
        {
            UserWord word = new UserWord(false, 2, "eng", "rus");


            Assert.IsTrue(word.Check("rus"));
        }

        [TestMethod]
        public void CheckDifferentTest()
        {
            UserWord word = new UserWord(false, 2, "eng", "rus");


            Assert.IsTrue(!word.Check("rqus"));
        }

        [TestMethod]
        public void UpdateTest()
        {
            UserWord word = new UserWord(false, 2, "eng", "rus");
            UserWord expected = new UserWord(true, 3, "eng", "rus");
            word.Upgrade();

            Assert.ReferenceEquals(word, expected);
        }
    }
}
