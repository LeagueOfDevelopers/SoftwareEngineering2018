using System;
using System.Collections.Generic;

namespace English.Domain
{
    public interface IUser : IStoredItem
    {
        string Name { get; }

        Dictionary<Word, int> LearningWords { get; }

        Dictionary<Word, int> LearnedWords { get; }

        void UpdateLearningWord(Word word);
    }
}
