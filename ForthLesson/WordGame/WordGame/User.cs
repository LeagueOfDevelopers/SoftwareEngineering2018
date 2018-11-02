using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class User : IUser
    {
        public User(Guid id, string name, Dictionary<string, int> inProcessWords, List<string> studiedWords)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            InProcessWords = inProcessWords ?? throw new ArgumentNullException(nameof(inProcessWords));
            StudiedWords = studiedWords ?? throw new ArgumentNullException(nameof(studiedWords));
        }

        public Guid Id { get; }
        public string Name { get; }
        public Dictionary<string, int> InProcessWords { get; private set; }
        public List<string> StudiedWords { get; }

        public void AddOneValueToProcess(WordForGame word)
        {
            var WordToStudy = InProcessWords.Where(x => x.Key == word.Word).First();
            InProcessWords.Remove(WordToStudy.Key);
            InProcessWords.Add(WordToStudy.Key, WordToStudy.Value + 1);
        }
        public bool WordAlreadyStudied(string Word)
        {
            if (StudiedWords.Contains(Word))
            {
                return true;
            }
            return false;
        }
        public bool WordStudiedNow(WordForGame word)
        {
            var wordForStudy = InProcessWords.Where(words => words.Key == word.Word).First();
            if (wordForStudy.Value == _countToLearnWord)
            {
                return true;
            }
            return false;
        }
        private int _countToLearnWord = 2;
    }
}
