using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.Application
{
    class WordManager : IWordManager
    {
        private readonly Random r;
        private IRepository<IPareOfWords> words;

        public WordManager(Random r, IRepository<IPareOfWords> words)
        {
            this.r = r ?? throw new ArgumentNullException(nameof(r));
            this.words = words ?? throw new ArgumentNullException(nameof(words));
        }

        public void AddWordOnLearning(IUser user, Guid wordId)
        {
            IPareOfWords word = words.Load(wordId);
            user.AddWordOnLearning(word);   
        }


        public IEnumerable<IPareOfWords> GetRandomWords(int count, IEnumerable<IPareOfWords> words)
        {
            if (words == null) throw new UncorrectArgumentException("words is null");
            return MixWords(words.Take(count));
        }

        public IEnumerable<IPareOfWords> MixWords(IEnumerable<IPareOfWords> words)
        {
            if (words == null) throw new UncorrectArgumentException("words is null");

            IPareOfWords[] newWords = words.ToArray();
            for (int i = 0; i < newWords.Length; i++)
            {
                int index = r.Next(0, newWords.Length);
                IPareOfWords t = newWords[index];
                newWords[index] = newWords[i];
                newWords[i] = t;
            }
            return newWords;
        }

        public IEnumerable<SessionPareOfWords> CangeHalfTranslations(IEnumerable<SessionPareOfWords> words)
        {
            if (words == null) throw new UncorrectArgumentException("words is null");

            IPareOfWords[] oldWords = words.ToArray();
            SessionPareOfWords[] newWords = new SessionPareOfWords[words.Count()];

            for (int i = 0; i <= oldWords.Length / 2; i += 2)
            {
                newWords[i] = new SessionPareOfWords(oldWords[i].Id,
                                                     oldWords[i].EnWord,
                                                     oldWords[i + 1].TransWord,
                                                     false,
                                                     false);
                newWords[i + 1] = new SessionPareOfWords(oldWords[i + 1].Id,
                                                     oldWords[i + 1].EnWord,
                                                     oldWords[i].TransWord,
                                                     false,
                                                     false);
            }
            return oldWords
                    .Skip(oldWords.Length / 2)
                    .Select(el => new SessionPareOfWords(el, false, true))
                    .Concat(newWords.Take(oldWords.Length / 2));
        }
    }
}
