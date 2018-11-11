using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishLearner.infrastructure;

namespace EnglishLearner.Application
{
    public class UserService : IUserService
    {
        private IRepository<IUser> users;
        private IWordManager wordManager;

        public UserService(IRepository<IUser> users, IWordManager wordManager)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.wordManager = wordManager ?? throw new ArgumentNullException(nameof(wordManager));
        }

        public Guid RegisterUser(string name)
        {
            Guid id = Guid.NewGuid();
            users.Save(new User(id, name, new UserPareOfWords[] { }));
            return id;
        }

        public IEnumerable<IPareOfWords> GetAllLearnedWords(Guid userId)
        {
            IUser user = users.Load(userId);
            return user.GetAllLearnedWords();
        }

        public void AddWordOnLearning(Guid userId, Guid wordId)
        {
            IUser user = users.Load(userId);
            wordManager.AddWordOnLearning(user, wordId);
            users.Save(user);
        }


        public IEnumerable<IPareOfWords> GetAllWordsOnLearning(Guid userId)
        {
            IUser user = users.Load(userId);
            return user.GetAllWordsOnLearning();
        }

        public static IUserService Create(Random r)
        {
            return new UserService(
                new Repository<IUser>(new User[] { }),
                new WordManager(r, new Repository<IPareOfWords>(new PareOfWords[] { })));
        }

        public void EditUser(Guid id, string name)
        {

        }
    }
}
