using System;
using System.Collections.Generic;

namespace Leo_sprint
{
    public interface IUser
    {
        Guid _id { get; }
        string _nickname { get; }
        void AddNewWordInDictionary(Word word);
        IEnumerable<Word> ShowWordInProgress();
        IEnumerable<Word> ShowLearnedWord();
    }
}
