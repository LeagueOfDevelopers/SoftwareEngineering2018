using Leo_sprint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SessionTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CheckRightAnswers_GetEmptyListOfWords()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { true, true };

            var list = test_user.CheckAnswers(answers, words_in_process.ToArray());
            var expected_list = new List<Word>();
            CollectionAssert.AreEqual(list, expected_list);
        }
        [TestMethod]
        public void CheckRightAnswersWordHaveFlagLessForOnePointUntillTheyBecameLearned_WordsBecameLearned()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 2), new Word("eng1", "rus1", 2) };            
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { true, true };

            var list = test_user.CheckAnswers(answers, words_in_process.ToArray());
            var list_of_learned_words = test_user.ShowLearnedWord().ToList();
            var expected_list = new List<Word> { new Word("eng", "rus", 3), new Word("eng1", "rus1", 3) };

            CollectionAssert.AreEqual(list_of_learned_words, expected_list);
        }
        [TestMethod]
        public void CheckRightAnswersWordHaveFlagLessForOnePointUntillTheyBecameLearned_ListWordsInPocessIsEmpty()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 2), new Word("eng1", "rus1", 2) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { true, true };

            var list = test_user.CheckAnswers(answers, words_in_process.ToArray());
            var list_word_in_process = test_user.ShowWordInProgress().ToList();
            var expected_list = new List<Word> ();

            CollectionAssert.AreEqual(expected_list, list_word_in_process);
        }
        [TestMethod]
        public void CheckRightAnswersWithOneWrongAnswer_GetListWithOneWord()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0) };
            var test_words = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus2", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { true, true };

            var list = test_user.CheckAnswers(answers, test_words.ToArray());
            var expected_list = new List<Word> { new Word("eng1", "rus1", 0) };
            CollectionAssert.AreEqual(list, expected_list);
        }
        [TestMethod]
        public void SecondCheckRightAnswers_GetEmptyListOfWords()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0) };
            var test_words = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus2", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { true, false };

            var list = test_user.CheckAnswers(answers, test_words.ToArray());
            var expected_list = new List<Word>();
            CollectionAssert.AreEqual(list, expected_list);
        }
        [TestMethod]
        public void SecondCheckRightAnswersWithOneWrongAnswer_GetListWithOneWord()
        {
            var words_in_process = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus1", 0) };
            var test_words = new List<Word> { new Word("eng", "rus", 0), new Word("eng1", "rus2", 0) };
            var test_user = new User(string.Empty, Guid.Empty, new List<Word>(), words_in_process);
            var answers = new bool[] { false, false };

            var list = test_user.CheckAnswers(answers,test_words.ToArray());
            var expected_list = new List<Word> { new Word("eng", "rus", 0) };
            CollectionAssert.AreEqual(list, expected_list);
        }


    }
}
