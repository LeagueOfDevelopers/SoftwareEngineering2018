using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class Session
    {
        private List<RepWord> _allWords { get; set; }
        private List<User> _users { get; set; }

        private void AddUser(User user)
        {
            _users.Add(user);
        }

        private void AddWord(RepWord word)
        {
            _allWords.Add(word);
        }

        private bool CheckWord(RepWord word)
        {
            return _allWords.Contains(word);
        }

        public void Registration(string name, Guid id)
        {
            Dictionary<string, UserWord> toReg = new Dictionary<string, UserWord>();
            if (_allWords.Count < 10) throw new InvalidOperationException();
            for (int i = 0; i < 10; i++)
            {
                toReg.Add(_allWords[i].Rus, new UserWord(false, 0, _allWords[i].Eng, _allWords[i].Rus));
            }
            AddUser(new User(name, id, new List<RepWord>(), toReg));
        }

        public void Start(User user, int wordCount)
        {
            Generator Gen = new Generator();
            List<RepWord> toWork = Gen.Start(wordCount, user);
            foreach (RepWord a in toWork)
            {
                bool UserAnswer = true; //вместо true ответ пользователя
                if (CheckWord(a) == UserAnswer)
                {
                    user.MarkAsLearned(a.Eng);
                }
            }

        }
    }
}
