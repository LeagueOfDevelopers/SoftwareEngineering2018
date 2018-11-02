using System;
using System.Collections;
using System.Collections.Generic;
using LoDSprint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LoDSprintTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void DeserializeDictionaryTest_GiveSerializedDictionary_GetDictionary()
        {
            var dictionary = new Dictionary<Word, Translation>
            {
                {
                    new Word("word"),
                    new Translation("translation")
                }
            };
            var serializedDictionary = JsonConvert
                .SerializeObject(dictionary);

            var deserializedDictionary = serializedDictionary
                .DeserializeDictionary();

            CollectionAssert.AreEqual(dictionary, deserializedDictionary);
        }
    }
}
