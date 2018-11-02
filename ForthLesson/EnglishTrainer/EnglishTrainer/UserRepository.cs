using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace EnglishTrainer
{
    public class UserRepository
    {
        public string _jsonFile { get; set; }

        public UserRepository(string file)
        {
            _jsonFile = file;
        }

        public List<User> GetUsers()
        {
            var rawFiles = File.ReadAllText(_jsonFile);
            var users = JsonConvert.DeserializeObject<List<User>>(rawFiles);
            return users;
        }

        public void SaveUsers(List<User> users)
        {
            var serialized = JsonConvert.SerializeObject(users);
            File.WriteAllText(_jsonFile, serialized);
        }

        public void SaveUser(User user)
        {
            var users = GetUsers();
            var newlist = new List<User>(users){user};
            SaveUsers(newlist);
        }
    }
}
