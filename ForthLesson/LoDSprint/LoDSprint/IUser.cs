using System;
using System.Collections.Generic;

namespace LoDSprint
{
    public interface IUser
    {
        Guid Id { get; }
        string NickName { get; }
        IEnumerable<Word> GetLearnedWords();
        IEnumerable<Word> GetStudiedWords();
        bool WordIsLearned(Word word);
        void SaveCorrectAnsweredWords(IEnumerable<Word> correctAnsweredWords);
        void SaveWrongAnsweredWords(IEnumerable<Word> wrongAnsweredWords);
    }
}
