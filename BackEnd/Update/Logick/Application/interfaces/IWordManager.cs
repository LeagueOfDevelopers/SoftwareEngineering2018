using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.Application
{
    public interface IWordManager
    {
        IEnumerable<SessionPareOfWords> GetRandomWords(IEnumerable<IPareOfWords> elements, int count);
        IEnumerable<SessionPareOfWords> MixTranslations(IEnumerable<SessionPareOfWords> words);
        void AddWordOnLearning(IUser user, Guid wordId);
        Guid AddWordToDictonary(string enWord, string translation);
        IEnumerable<IPareOfWords> Words { get; }
    }
}
