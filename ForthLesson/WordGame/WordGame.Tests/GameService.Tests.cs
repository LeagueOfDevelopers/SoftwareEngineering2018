using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordGame.Application;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WordGame.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void GetWordWhichNotAlreadyStudied_GetNewWord()
        {
            GameService gameService = GetSimpleGameServiceWithUser();
            WordForGame Word = gameService.GetWord(_idOfUser);
            string CurrentWord = "Kiwi";
            string ExpectedWord = Word.Word;

            Assert.AreEqual(CurrentWord, ExpectedWord);
        }
        [TestMethod]
        public void CheckForStudiedNowWord_AddStudiedNowWord()
        {
            GameService gameService = GetSimpleGameServiceWithUser();
            WordForGame word = new WordForGame("Apple", "Яблоко", "Киви");
            gameService.CheckAnswer(_idOfUser, word);
            string[] Users = File.ReadAllLines("Users.json");
            List<User> AllUsers = Users
                .Select(JsonUser => JsonConvert.DeserializeObject<User>(JsonUser))
                .ToList();
            User sameUser = AllUsers.Find(userWith => userWith.Id == _idOfUser);

            Assert.IsTrue(sameUser.InProcessWords.ContainsValue(2));
        }
        [TestMethod]
        public void AddNowStudiedWord_AddWordToAnotherDictionary()
        {
            GameService gameService = GetSimpleGameServiceWithUser();
            WordForGame word = new WordForGame("Apple", "Яблоко", "Киви");
            gameService.CheckAnswer(_idOfUser, word);
            string[] Users = File.ReadAllLines("Users.json");
            List<User> AllUsers = Users
                .Select(JsonUser => JsonConvert.DeserializeObject<User>(JsonUser))
                .ToList();
            User sameUser = AllUsers.Find(userWith => userWith.Id == _idOfUser);

            Assert.IsTrue(sameUser.StudiedWords.Contains("Apple"));
        }
        public GameService GetSimpleGameServiceWithUser()
        {
            File.Delete("Dictionary.json");
            File.Delete("Users.json");
            DictionaryRepository dictionaryRepository = new DictionaryRepository();
            dictionaryRepository.SaveWord("Apple", "яблоко");
            dictionaryRepository.SaveWord("Kiwi", "киви");
            UserRepository userRepository = new UserRepository();
            Guid idOfUser = _idOfUser;
            Dictionary<string, int> InProcessWords = new Dictionary<string, int>
            {
                { "Apple", 2 }
            };
            List<string> StudiedWords = new List<string> { "Apple" };
            User user = new User(idOfUser, "Vasya", InProcessWords, StudiedWords);
            userRepository.SaveUser(user);
            GameService gameService = new GameService(userRepository, dictionaryRepository);
            return gameService;
        }
        Guid _idOfUser = Guid.NewGuid();
    }
}
