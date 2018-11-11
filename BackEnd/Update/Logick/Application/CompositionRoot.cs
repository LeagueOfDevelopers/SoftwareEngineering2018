using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishLearner.infrastructure;

namespace EnglishLearner.Application
{
    public class CompositionRoot
    {
        public ISessionManager SessionManager { get; private set; }
        public IUserService UserService { get; private set; }

        public static CompositionRoot Configurate(Random r, string UserPath, string WordsPath)
        {
            IRepository<IUser> users = new Repository<IUser>(new User[] { });
            IRepository<ISession> sessions = new Repository<ISession>(new Session[] { });
            IRepository<IPareOfWords> words = new Repository<IPareOfWords>(new PareOfWords[] { });
            IWordManager wordManager = new WordManager(r, words);
            
            return new CompositionRoot
            {
                SessionManager = new SessionManager(users, sessions, wordManager, 10),
                UserService = new UserService(users, wordManager),
            };
        }     
    }
}
