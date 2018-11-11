using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class Session : ISession
    {
        public Guid Id { get; private set; }
        public bool IsOpen { get; private set; }
        public IUser User { get; private set; }
        private SessionPareOfWords[] words;

        public Session(Guid id, bool isOpen, IUser user, SessionPareOfWords[] words)
        {
            Id = id;
            IsOpen = isOpen;
            User = user ?? throw new ArgumentNullException(nameof(user));
            this.words = words ?? throw new ArgumentNullException(nameof(words));
        }

        public int SaveResults(Dictionary<Guid,string> answers)
        {
            int countOfRightAnswers = 0;
            foreach(Guid id in answers.Keys)
            {
                if (answers[id] == words.First(word => word.Id == id).Answer)
                {
                    User.LearnWord(id);
                    countOfRightAnswers++;
                }
            }
            IsOpen = false;
            return countOfRightAnswers;
        }

        public static Session CreateSession(IUser user, Guid id)
        {
            return new Session(id, false, user, new SessionPareOfWords[0])
            {
                Id = id,
                User = user ?? throw new ArgumentNullException(nameof(user)),
                IsOpen = false,
                words = new SessionPareOfWords[0]
            };
        }


        public void Start(IEnumerable<SessionPareOfWords> words, IUser user)
        {
            this.words = words.ToArray();
            User = user;
            IsOpen = true;
        }

        public IEnumerable<IPareOfWords> GetExercise()
        {
            return words;
        }

        public IEnumerable<IPareOfWords> GetAllWordPares()
        {
            return words;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
