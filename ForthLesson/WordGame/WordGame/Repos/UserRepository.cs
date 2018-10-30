using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace WordGame
{
    public class UserRepository : IUserRepository
    {
        public User LoadUser(Guid userId)
        {
            List<User> AllUsers = DeserializeUsers();
            if (!AllUsers.Exists(userWith => userWith.Id == userId))
            {
                throw new Exception($"User with id {userId} not found");
            }
            User user = AllUsers.Find(userWith => userWith.Id == userId);
            return user;
        }
        public string SerializeUser(User user)
        {
            string userToJson = JsonConvert.SerializeObject(user);
            return userToJson;
        }
        public List<User> DeserializeUsers()
        {
            string[] Users = File.ReadAllLines(path);
            List<User> AllUsers = Users
                .Select(JsonUser => JsonConvert.DeserializeObject<User>(JsonUser))
                .ToList();
            return AllUsers;
        }
        public void UpdateUser(User user)
        {
            List<User> AllUsers = DeserializeUsers();
            User userToUpdate = AllUsers.Find(userWith => userWith.Id == user.Id);
            AllUsers.Remove(userToUpdate);
            AllUsers.Add(user);
            File.Delete(path);
            AllUsers.ForEach(x => SaveUser(x));
        }
        public void SaveUser(User user)
        {
            string userToJson = SerializeUser(user);
            File.AppendAllText(path, userToJson + '\n');
        }

        private string path = "Users.json";
    }
}
