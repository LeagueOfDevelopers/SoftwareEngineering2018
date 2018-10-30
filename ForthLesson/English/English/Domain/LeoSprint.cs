using System;
using System.Collections.Generic;
using System.Linq;
using English.Infrastructure;

namespace English.Domain
{
    public class LeoSprint : IExercise
    {
        public LeoSprint(Guid id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public Guid Id { get; }

        public string Name { get; }

        public ValueTuple<List<Word>, List<Word>> GetWordsFor(IUser user, int amount)
        {
            var rand = new Random();

            var originalWords = user.LearningWords.ToList()
                .ShuffleWordsAndTakeKeys(amount);

            var anotherWords = user.LearningWords.ToList()
                .FindAll(x => !originalWords.Contains(x.Key))
                .ShuffleWordsAndTakeKeys(amount);

            return new ValueTuple<List<Word>, List<Word>>(originalWords, anotherWords);
        }

        public bool GuessWord(IUser user, IWord translation, string original)
        {
            var result = translation.Translation == original;

            if (result)
            {
                user.UpdateLearningWord(translation as Word);
            }

            return result;
        }
    }
}
