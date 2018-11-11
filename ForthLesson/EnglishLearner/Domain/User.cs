using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class User : IUser
    {
        private Guid id;
        private string name;
        private List<UserPareOfWords> words;

        public User(Guid id, string name, IEnumerable<UserPareOfWords> words)
        {
            this.id = id;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.words = words.ToList() ?? throw new ArgumentNullException(nameof(words));
        }

        public IEnumerable<UserPareOfWords> Words => words;
        public Guid Id => id;
        public string Name => name;

        public void AddWordOnLearning(IPareOfWords word)
        {
            UserPareOfWords newWord = new UserPareOfWords(word.Id, word.EnWord, word.TransWord, 0);
            words.Add(newWord);
        }

        public void LearnWord(Guid id)
        {
            words.First(el => el.Id == id).LearnWord();
        }

        public IEnumerable<IPareOfWords> GetAllLearnedWords()
        {
            return Words.Where(el => el.DepthOfLearning == 3);
        }

        public IEnumerable<IPareOfWords> GetAllWordsOnLearning()
        {
            return Words.Where(el => el.DepthOfLearning != 3);
        }
    }
}
