using System;
using System.Collections.Generic;
using System.Linq;
using Leo_sprint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SessionTest
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void CreateSession_GetMixedListOfWord()
        {
            var words = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0), new Word("eng2", "rus2", 0), new Word("eng3", "rus3", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words);
            var session = Session.Create(words.ToArray());
            var task = words.Select(word => word._in_english + "-" + word._in_russian);
            
            CollectionAssert.AreNotEqual(session.ShowTask().ToList(), task.ToList());
        }
        [TestMethod]
        public void CreateSession_GetMixedListOf5Word()
        {
            var words = new List<Word> {
                new Word("eng", "rus", 0),
                new Word("eng1", "rus1", 0),
                new Word("eng2", "rus2", 0),
                new Word("eng3", "rus3", 0),
                new Word("eng4", "rus4", 0),
                new Word("eng5", "rus5", 0),
                new Word("eng6", "rus6", 0),
                new Word("eng7", "rus7", 0)
            };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words);
            var session = test_user.StartSession(5);
            Assert.IsTrue(session.ShowTask().Count() == 5);
        }

    }
}
