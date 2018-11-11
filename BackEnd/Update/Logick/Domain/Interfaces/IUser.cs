using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface IUser : IIdentificator
    {
        string Name { get; }
        IEnumerable<UserPareOfWords> Words { get; }
        void LearnWord(Guid id);
        void AddWordOnLearning(IPareOfWords word);
        IEnumerable<IPareOfWords> GetAllLearnedWords();
        IEnumerable<IPareOfWords> GetAllWordsOnLearning();
    }
}
