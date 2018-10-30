using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnglishLessons
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void UserTest1()
        {
            Guid id = new Guid();
            var DictToAdd = new Dictionary<string, UserWord>();
            DictToAdd.Add("FooBar", new UserWord(false, 0, "FooBar", "говнокод"));

            User user = new User("name", id, new List<RepWord>(), DictToAdd);
            var Expected = new List<RepWord>();
            Expected.Add(new RepWord("FooBar", "говнокод"));
            var toTest = user.ShowInProgress();
            CollectionAssert.ReferenceEquals(toTest, Expected);
        }
    }
}
