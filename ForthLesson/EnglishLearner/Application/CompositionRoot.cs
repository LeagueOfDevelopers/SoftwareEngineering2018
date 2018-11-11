using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.Application
{
    class CompositionRoot
    {
        public ISessionManager SessionManager { get; private set; }
        public IUserService UserService { get; private set; }
        public IDataManager DataManager { get; private set; }

        public static CompositionRoot Configurate(Random r, string UserPath, string WordsPath)
        {
            IRepository<IUser> users = new Repository<IUser>(new IUser[] { });
            IRepository<ISession> sessions = new Repository<ISession>(new ISession[] { });
            IRepository<IPareOfWords> words = new Repository<IPareOfWords>(new PareOfWords[] { });
            IWordManager wordManager = new WordManager(r, words);
            
            return new CompositionRoot
            {
                SessionManager = new SessionManager(users, sessions, wordManager),
                UserService = new UserService(users, wordManager),
                DataManager = new DataManager(users, words, UserPath, WordsPath)
            };
        }     
    }
}
