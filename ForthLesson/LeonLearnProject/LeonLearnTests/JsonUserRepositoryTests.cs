using System;
using System.Collections.Generic;
using System.Security.Authentication;
using LeonLearn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonLearnTests
{
    [TestClass]
    public class JsonUserRepositoryTests
    {
        [TestMethod]
        public void AddUser()
        {
            var userRepo = new JsonUserRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestUsers.json");
            var newId = Guid.NewGuid();
            var user = new User(newId,
                "LOH",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );


            userRepo.AddUser(user);

            var returned = userRepo.GetUser(newId);

            Assert.IsTrue(user == returned);

            userRepo.DeleteUser(newId);
        }


        [TestMethod]
        public void GetLearnedWords()
        {
            var userRepo = new JsonUserRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestUsers.json");
            var user = userRepo.GetUser(Guid.Empty);
            var learned = user.LearnedWords;

            List<WordPair> expected = new List<WordPair>()
            {
                new WordPair(
                    "Hell",
                    "Ад"
                )
            };

            CollectionAssert.AreEqual(expected, learned);
        }

        [TestMethod]
        public void DeleteUser()
        {
            var userRepo = new JsonUserRepository(@"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestUsers.json");

            var newId = Guid.NewGuid();

            var user = new User(newId,
                "LOH",
                DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>()
            );
            userRepo.AddUser(user);

            userRepo.DeleteUser(newId);

            Assert.ThrowsException<AuthenticationException>(() => { userRepo.GetUser(newId); });
        }
    }
}