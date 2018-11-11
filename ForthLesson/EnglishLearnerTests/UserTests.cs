using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnglishLearner;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearnerTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AddWordOnLearning_wordWillAddInUsersWords()
        {
            IUser user = new User(Guid.NewGuid(), "Me", new UserPareOfWords[] { });
            IPareOfWords word = new PareOfWords(Guid.NewGuid(), "apple", "яблоко");
            IEnumerable<IPareOfWords> expAnsw = new IPareOfWords[] { word };

            user.AddWordOnLearning(word);

            PareOfWords[] words1 = user.GetAllWordsOnLearning().Cast<PareOfWords>().ToArray();
            PareOfWords[] words2 = expAnsw.Cast<PareOfWords>().ToArray();
            
            CollectionAssert.AreEqual(words1, words2);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            UserPareOfWords word = new UserPareOfWords(Guid.NewGuid(), "apple", "яблоко", 0);
            IUser user = new User(Guid.NewGuid(), "Me", new UserPareOfWords[] { word });
            int expAnsw = 1;

            user.LearnWord(word.Id);

            Assert.IsTrue(user.Words.First().DepthOfLearning == expAnsw);
        }
    }
}
