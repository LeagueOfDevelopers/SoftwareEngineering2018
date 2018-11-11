using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    class SessionManager : ISessionManager
    {
        private IRepository<IUser> users;
        private IRepository<ISession> sessions;
        private IWordManager wordManager;

        public SessionManager(IRepository<IUser> users, IRepository<ISession> sessions, IWordManager wordManager)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            this.wordManager = wordManager ?? throw new ArgumentNullException(nameof(wordManager));
        }

        public IEnumerable<SessionPareOfWords> GetExercise(Guid sessionId)
        {
            ISession session = sessions.Load(sessionId);
            return session.GetExercise();
        }

        public void SaveResults(Guid sessionId)
        {
            ISession session = sessions.Load(sessionId);

            IEnumerable<IPareOfWords> rightAnswers = session.GetAllWordPares()
                .Where(el => el.IsAnswerRight == true);

           foreach(IPareOfWords word in rightAnswers)
            {
                session.User.LearnWord(word.Id);
            }

        }

        public void StartSession(Guid sessionId)
        {
            ISession session = sessions.Load(sessionId);
            IEnumerable<SessionPareOfWords> words = MixWords(GetValidWords(session.User));
            
            session.Start(words, session.User);
            sessions.Save(session);
        }

        public Guid CreateSession(Guid userId)
        {
            Guid id = Guid.NewGuid();
            IUser user = users.Load(userId);
            
            Session session = Session.CreateSession(user, id);
            sessions.Save(session);
            return id;
        }

        private IEnumerable<IPareOfWords> GetValidWords(IUser user)
        {
            return user.Words.Where(el => el.DepthOfLearning < 3);    
        }

        private IEnumerable<SessionPareOfWords> MixWords(IEnumerable<IPareOfWords> words)
        {
            words = wordManager.MixWords(words);
            words = wordManager.CangeHalfTranslations(words.Cast<SessionPareOfWords>());
            words = wordManager.MixWords(words);
            return (IEnumerable<SessionPareOfWords>)words;
        }
    }
}
