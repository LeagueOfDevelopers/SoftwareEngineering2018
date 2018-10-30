using System;
using System.Collections.Generic;
using System.Linq;

namespace LoDSprint
{
    public class User : IUser
    {
        public User(Guid id, string nickName, List<Word> learnedWords, Dictionary<Word, int> studiedWords)
        {
            Id = id;
            NickName = nickName ?? throw new ArgumentNullException(nameof(nickName));
            _learnedWords = learnedWords ?? throw new ArgumentNullException(nameof(learnedWords));
            _studiedWords = studiedWords ?? throw new ArgumentNullException(nameof(studiedWords));
        }

        public Guid Id { get; }

        public string NickName { get; }

        public IEnumerable<Word> GetLearnedWords()
        {
            return _learnedWords;
        }

        public IEnumerable<Word> GetStudiedWords()
        {
            return _studiedWords.Keys;
        }

        public bool WordIsLearned(Word word)
        {
            return _learnedWords.Contains(word);
        }

        public void SaveWrongAnsweredWords(IEnumerable<Word> wrongAnsweredWords)
        {
            foreach(var word in wrongAnsweredWords)
            {
                if (!_studiedWords.ContainsKey(word))
                    _studiedWords[word] = 0;
            }
        }

        public void SaveCorrectAnsweredWords(IEnumerable<Word> correctAnsweredWords)
        {
            foreach(var word in correctAnsweredWords)
            {
                if (_studiedWords.ContainsKey(word))
                    _studiedWords[word]++;
                else
                    _studiedWords[word] = 1;
            }

            CheckNewLearnedWords();
        }

        private void CheckNewLearnedWords()
        {
            var learnedWords = _studiedWords
                .Where(pair => 
                    pair.Value >= 3)
                .Select(learnedPair => 
                    learnedPair.Key);
            _learnedWords.AddRange(learnedWords);

            foreach (var word in learnedWords)
                _studiedWords
                    .Remove(word);
        }

        private List<Word> _learnedWords;
        private Dictionary<Word, int> _studiedWords;
    }
}
