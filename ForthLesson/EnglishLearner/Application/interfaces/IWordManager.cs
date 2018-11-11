using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface IWordManager
    {
        IEnumerable<IPareOfWords> GetRandomWords(int count, IEnumerable<IPareOfWords> words);
        IEnumerable<IPareOfWords> MixWords(IEnumerable<IPareOfWords> words);
        IEnumerable<SessionPareOfWords> CangeHalfTranslations(IEnumerable<SessionPareOfWords> words);
        void AddWordOnLearning(IUser user, Guid wordId);

    }
}
