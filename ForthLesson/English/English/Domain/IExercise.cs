using System;
using System.Collections.Generic;

namespace English.Domain
{
    public interface IExercise : IStoredItem
    {
        ValueTuple<List<Word>, List<Word>> GetWordsFor(IUser user, int amount);

        bool GuessWord(IUser user, IWord translation, string original);
    }
}
