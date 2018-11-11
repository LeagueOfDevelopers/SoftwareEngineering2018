using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserContext
{
    public class JsonUserRepository : IUserRepository
    {
        public string Path { get; set; }

        public JsonUserRepository(string path)
        {
            Path = path;
        }

        public static JsonUserRepository Default =
            new JsonUserRepository(@"/Users/leon/Projects/LeonLearn/LeonLearn/Users.json");

        public User GetUser(Guid userId)
        {
            if (!IsUser(userId)) throw new AuthenticationException("No such user");

            var allUsers = JArray.Parse(File.ReadAllText(Path)).ToObject<User[]>();
            return allUsers.First(user => user.Id == userId);
        }

        public void AddUser(User user)
        {
            if (IsUser(user.Id)) throw new InvalidOperationException("User already exists");

            var allUsers = JArray.Parse(File.ReadAllText(Path)).ToObject<List<User>>();
            allUsers.Add(user);

            File.WriteAllText(Path, JsonConvert.SerializeObject(allUsers));
        }

        public void DeleteUser(Guid userId)
        {
            if (!IsUser(userId)) throw new AuthenticationException("No such user");

            var allUsers = JArray.Parse(File.ReadAllText(Path)).ToObject<User[]>();
            var newUsers = allUsers.Where(user => user.Id != userId);

            File.WriteAllText(Path, JsonConvert.SerializeObject(newUsers));
        }

        public void EditUser(User editedUser)
        {
            if (!IsUser(editedUser.Id)) throw new AuthenticationException("No such user");

            var allUsers = JArray.Parse(File.ReadAllText(Path)).ToObject<User[]>();
            var newUsers = allUsers.Where(user => user.Id != editedUser.Id).Append(editedUser);

            File.WriteAllText(Path, JsonConvert.SerializeObject(newUsers));
        }

        public bool IsUser(Guid userId)
        {
            var allUsers = JArray.Parse(File.ReadAllText(Path)).ToObject<User[]>();
            return allUsers.Any(user => user.Id == userId);
        }
    }
}