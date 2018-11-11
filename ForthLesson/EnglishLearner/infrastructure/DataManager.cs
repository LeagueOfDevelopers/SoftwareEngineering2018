using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace EnglishLearner
{
    public class DataManager : IDataManager
    {
        private IRepository<IUser> users;
        private IRepository<IPareOfWords> words;
        private string userPath;
        private string wordsPath;

        public DataManager(IRepository<IUser> users, IRepository<IPareOfWords> words, string userPath, string wordsPath)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.words = words ?? throw new ArgumentNullException(nameof(words));
            this.userPath = userPath ?? throw new ArgumentNullException(nameof(userPath));
            this.wordsPath = wordsPath ?? throw new ArgumentNullException(nameof(wordsPath));
        }

        public void LoadUsers()
        {
            IEnumerable<User> loadedUsers = LoadItems<User>(userPath);
            foreach (IUser user in loadedUsers)
            {
                users.Save(user);
            }
        }

        public void LoadWords()
        {
            IEnumerable<PareOfWords> loadedWords = LoadItems<PareOfWords>(wordsPath);
            foreach(IPareOfWords word in loadedWords)
            {
                words.Save(word);
            }
        }

        private IEnumerable<T> LoadItems<T>(string path)
        {
            string[] JsonItems = File.ReadAllLines(wordsPath);
            return JsonItems.Select(el => JsonConvert.DeserializeObject(el)).Cast<T>();
        }

        public void SaveUsers()
        {
            string[] usersJson = users.GetAll().Cast<PareOfWords>().Select(el => JsonConvert.SerializeObject(el)).ToArray();
            File.WriteAllLines(userPath, usersJson);
        }

        public void SaveWords()
        {
            string[] usersJson = words.GetAll().Cast<PareOfWords>().Select(el => JsonConvert.SerializeObject(el)).ToArray();
            File.WriteAllLines(wordsPath, usersJson);
        }
    }
}
