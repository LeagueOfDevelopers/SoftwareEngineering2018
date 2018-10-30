﻿using System;
using System.Collections.Generic;
using Leo_sprint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SessionTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CheckRightAnswers_GetEmptyListOfWords()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(),words_in_process );
            var answers = new bool[] { true, true };

            var list = test_user.CheckAnswers(answers, words_in_process.ToArray());
            var expected_list = new List<Word>();
            CollectionAssert.ReferenceEquals(list, expected_list);
        }
    }
}
