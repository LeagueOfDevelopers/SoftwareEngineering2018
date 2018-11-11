using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishLearner.infrastructure;

namespace EnglishLearner.Application
{
    public class SessionManager : ISessionManager
    {
        private IRepository<IUser> users;
        private IRepository<ISession> sessions;
        private IWordManager wordManager;
        private int sizeExercise;

        public SessionManager(IRepository<IUser> users, IRepository<ISession> sessions, IWordManager wordManager, int sizeExercise)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            this.wordManager = wordManager ?? throw new ArgumentNullException(nameof(wordManager));
            this.sizeExercise = sizeExercise;
        }

        public SessionManager(IRepository<IUser> users, IWordManager wordManager, int sizeExercise)
        {
            sessions = new Repository<ISession>(new Session[] { });
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.wordManager = wordManager ?? throw new ArgumentNullException(nameof(wordManager));
            this.sizeExercise = sizeExercise;
        }

        public IEnumerable<IPareOfWords> GetExercise(Guid sessionId)
        {
            ISession session = sessions.Load(sessionId);
            return session.GetExercise();
        }

        public int SaveResults(Guid sessionId, Dictionary<Guid,string> answers)
        {
            ISession session = sessions.Load(sessionId);
            int rightAnswers = session.SaveResults(answers);

            users.Save(session.User);
            sessions.Save(session);
            return rightAnswers;
        }

        public IEnumerable<IPareOfWords> StartSession(Guid sessionId)
        {
            ISession session = sessions.Load(sessionId);
            IEnumerable<SessionPareOfWords> sesWords = wordManager.GetRandomWords(
                     session.User.Words, sizeExercise);
            IEnumerable<SessionPareOfWords> words = wordManager.MixTranslations(sesWords);

            session.Start(words, session.User);
            sessions.Save(session);
            return words;
        }

        public Guid CreateSession(Guid userId)
        {
            Guid id = Guid.NewGuid();
            IUser user = users.Load(userId);
            
            Session session = Session.CreateSession(user, id);
            sessions.Save(session);
            return id;
        }

        public IEnumerable<ISession> GetSessions()
        {
            return sessions.GetAll();
        }

        public void CloseSession(Guid id)
        {
            ISession session = sessions.Load(id);
            session.Close();
        }
    }
}
