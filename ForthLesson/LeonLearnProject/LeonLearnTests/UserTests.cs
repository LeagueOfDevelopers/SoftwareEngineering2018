using System;
using System.Collections.Generic;
using LeonLearn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonLearnTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserEquals()
        {
            var sameId = Guid.NewGuid();
            var first = new User(sameId,
                "first",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );
            var second = new User(sameId,
                "second",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );

            Assert.AreEqual(first, second);
        }
        
        [TestMethod]
        public void UserNotEqual()
        {
            var first = new User(Guid.NewGuid(), 
                "first",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );
            var second = new User(Guid.NewGuid(), 
                "second",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );

            Assert.AreNotEqual(first, second);
        }
    }
}