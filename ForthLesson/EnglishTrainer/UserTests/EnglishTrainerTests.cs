using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnglishTrainer;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTrainerTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AddNewWordAtWordsRepository_NewWordAtWordsRepository()
        {
            Word word = new Word(Guid.NewGuid(), "tea", "чай", 0);
            List<Word> inMemoryWords = new List<Word>();
            User user = new User("Artem", Guid.NewGuid(), inMemoryWords);
            UserRepository userRepository = new UserRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/user.json");
            WordsRepository wordsRepository = new WordsRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/testWords.json");
            UserService userService = new UserService(wordsRepository, userRepository);
            Session session = new Session(user, wordsRepository, userService);
            UserFacade userFacade = new UserFacade(userService, session);

            userFacade.AddWordAtUserWordsRepository(user, word);
            List<Word> expected = new List<Word>() { word };
            List<Word> arr = new List<Word>();

            arr = user._inMemoryWords.Where(item => item.Id_Word == word.Id_Word).Select(item => item).ToList();

            Equals(arr, expected);

        }

        [TestMethod]
        public void UserName_Registration()
        {
            Word word1 = new Word(Guid.NewGuid(), "tea", "чай", 0);
            List<Word> inMemoryWords = new List<Word>();
            User user = new User("Artem", Guid.NewGuid(), inMemoryWords);
            UserRepository userRepository = new UserRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/user.json");
            WordsRepository wordsRepository = new WordsRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/testWords.json");
            UserService userService = new UserService(wordsRepository, userRepository);
            Session session = new Session(user, wordsRepository, userService);
            UserFacade userFacade = new UserFacade(userService, session);

            userFacade.Registration("Artem");
            List<string> expected = new List<string>() { "Artem" };
            List<string> arr = new List<string>();
            var users = userRepository.GetUsers();
            users.ForEach((itemUser) =>
            {
                if (itemUser.Name == "Artem")
                {
                    arr.Add(itemUser.Name);
                }
            });

            Equals(arr, expected);
        }

        [TestMethod]
        public void EnWordRuWord_AddWordAtWordsRepository()
        {
            Word word1 = new Word(Guid.NewGuid(), "tea", "чай", 0);
            List<Word> inMemoryWords = new List<Word>();
            User user = new User("Artem", Guid.NewGuid(), inMemoryWords);
            UserRepository userRepository = new UserRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/user.json");
            WordsRepository wordsRepository = new WordsRepository(@"/Users/odmen/Projects/EnglishTrainer/EnglishTrainer/bin/testWords.json");
            UserService userService = new UserService(wordsRepository, userRepository);
            Session session = new Session(user, wordsRepository, userService);
            UserFacade userFacade = new UserFacade(userService, session);
            string enWord = "table";
            string ruWord = "стол";

            userFacade.AddWordWordsRepository(enWord, ruWord);

            List<string> expected = new List<string>() { "table" };
            List<string> arr = new List<string>();

            var dict = wordsRepository.GetWords();

            arr = dict.Where(item => item._enWord == "table")
                      .Select(item => item._enWord).ToList();

            Equals(arr, expected);
        }
    }
}
