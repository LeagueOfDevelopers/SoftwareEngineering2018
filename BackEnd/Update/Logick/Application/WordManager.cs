using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishLearner.infrastructure;
namespace EnglishLearner.Application
{
    public class WordManager : IWordManager
    {
        private readonly Random r;
        private IRepository<IPareOfWords> words;

        public IEnumerable<IPareOfWords> Words => words.GetAll();

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

        public IEnumerable<SessionPareOfWords> MixTranslations(IEnumerable<SessionPareOfWords> words)
        {
            SessionPareOfWords[] mixedWords = words.ToArray();
            int halfLength = mixedWords.Length / 2;

            for (int i = 0; i < halfLength; i++)
            {
                int index = r.Next(mixedWords.Length);
                string t = mixedWords[i].TransWord;
                mixedWords[i].TransWord = mixedWords[index].TransWord;
                mixedWords[index].TransWord = t;
            }
            return mixedWords;
        }

        public Guid AddWordToDictonary(string enWord, string translation)
        {
            Guid id = Guid.NewGuid();
            PareOfWords word = new PareOfWords(id, enWord, translation);
            words.Save(word);
            return id;
        }

        public IEnumerable<SessionPareOfWords> GetRandomWords(IEnumerable<IPareOfWords> elements, int count)
        {
            IPareOfWords[] newWords = elements.ToArray();
            for(int i = 0; i< newWords.Length; i++)
            {
                int index = r.Next(newWords.Length);
                IPareOfWords t = newWords[i];
                newWords[i] = newWords[index];
                newWords[index] = newWords[i];
            }



            return newWords.Take(count).Select(el => new SessionPareOfWords(el));
        }
    }
}
