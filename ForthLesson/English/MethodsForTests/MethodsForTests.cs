using System;
using System.Collections.Generic;
using System.Linq;
using English.Domain;
using English.Infrastructure;

namespace MethodsForTests
{
    public static class MethodsForTests
    {
        public static User CreateUserWithWord(Guid wordId, int countToBeLearned)
        {
            var user = new User(Guid.NewGuid(), "Bob",
                new Dictionary<Word, int>(), new Dictionary<Word, int>())
            {
                LearningWords = GetTestWordsWith(wordId, countToBeLearned)
                .ToDictionary(word => word as Word, _ => 0)
            };
            UserDatabase.GetInstance().SaveUserToFile(user);
            return user;
        }

        public static List<IWord> GetTestWordsWith(Guid id, int countToBeLearned)
        {
            return new List<IWord>
            {
                new Word(id, "Behind", "Сзади", countToBeLearned),
                new Word(Guid.NewGuid(), "Helium", "Гелий", 2),
                new Word(Guid.NewGuid(), "Genius", "Гений", 2),
                new Word(Guid.NewGuid(), "Teacup", "Чашка", 2),
                new Word(Guid.NewGuid(), "Trance", "Транс", 2)
            };
        }
    }
}
