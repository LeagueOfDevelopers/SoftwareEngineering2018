using System;
using System.Collections.Generic;
using LoDSprint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LoDSprintTests
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void SerializeDictionaryTest_GiveDictionary_GetCorrectSerializedString()
        {
            var dictionary = new Dictionary<Word, Translation>
            {
                {
                    new Word("word"),
                    new Translation("translation")
                }
            };
            var correctSerializedString = JsonConvert.SerializeObject(dictionary);

            var serializedString = dictionary.SerializeDictionary();

            Assert.AreEqual(correctSerializedString, serializedString);
        }

        [TestMethod]
        public void SerializeDictionaryTest_GiveUserDictionary_GetCorrectSerializedString()
        {
            var userDictionary = new Dictionary<Guid, IUser>
            {
                {
                    Guid.NewGuid(),
                    new User(
                        Guid.NewGuid(),
                        "user",
                        new List<Word>(),
                        new Dictionary<Word, int>())
                }
            };
            var correctSerializedString = JsonConvert.SerializeObject(userDictionary);

            var serializedString = userDictionary.SerializeUsers();

            Assert.AreEqual(correctSerializedString, serializedString);
        }
    }
}
