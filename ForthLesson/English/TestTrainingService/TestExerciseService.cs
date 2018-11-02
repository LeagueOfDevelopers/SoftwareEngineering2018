using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using English.Application;
using English.Domain;
using English.Infrastructure;
using static MethodsForTests.MethodsForTests;

namespace TestExerciseService
{
    [TestClass]
    public class TestExerciseService
    {
        [TestMethod]
        public void TestGetWords_ShuffledWords()
        {
            var initialSize = 2;
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var leoSprint = CreateLeoSprint();
            var exerciseService = CreateExerciseService(leoSprint);

            var (originalWords, randomWords) = exerciseService.GetWords(leoSprint.Id, user.Id, initialSize);

            Assert.AreEqual(randomWords.Count(), initialSize);
            Assert.AreEqual(originalWords.Count(), initialSize);
            randomWords.ToList()
                .ForEach(word => originalWords.ToList()
                    .ForEach(word2 => Assert.AreNotEqual(word.Body, word2.Body)));
        }

        [TestMethod]
        public void RightGuess_True()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var leoSprint = CreateLeoSprint();
            var exerciseService = CreateExerciseService(leoSprint);

            var result = exerciseService.GuessWord(leoSprint.Id, user.Id, "Сзади", wordId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WrongGuess_False()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var leoSprint = CreateLeoSprint();
            var exerciseService = CreateExerciseService(leoSprint);

            var result = exerciseService.GuessWord(leoSprint.Id, user.Id, "Гений", wordId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GuessWord_LearnedWordInUser()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var leoSprint = CreateLeoSprint();
            var exerciseService = CreateExerciseService(leoSprint);

            var result = exerciseService.GuessWord(leoSprint.Id, user.Id, "Сзади", wordId);

            Assert.AreEqual(exerciseService.GetUserById(user.Id).LearnedWords.Count(), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GuessNotExistingWord_Exception()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var leoSprint = CreateLeoSprint();
            var exerciseService = CreateExerciseService(leoSprint);

            var result = exerciseService.GuessWord(leoSprint.Id, user.Id, "Сзади", Guid.NewGuid());
        }

        private ExerciseService CreateExerciseService(LeoSprint leoSprint)
        {
            var exerciseRepository = new ItemRepository<IExercise>(new List<IExercise> { leoSprint });
            return new ExerciseService(exerciseRepository);
        }

        private LeoSprint CreateLeoSprint()
        {
            return new LeoSprint(Guid.NewGuid(), "LeoSprint");
        }
    }
}