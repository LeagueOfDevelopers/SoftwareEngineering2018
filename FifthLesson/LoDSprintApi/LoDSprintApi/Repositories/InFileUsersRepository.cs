using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoDSprintApi.Repositories
{
    public class InFileUsersRepository : IUsersRepository
    {
        public InFileUsersRepository()
        {
            _filePath = "C:/Users/Давид/source/repos/LoDSprintApi/UsersRepository";
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "");
        }

        public UserModel LoadUser(Guid userId)
        {
            return DeserializeUsers()
                .Find(user => 
                    user.Id == userId);
        }

        public void SaveUser(UserModel user)
        {
            var users = DeserializeUsers() ?? new List<UserModel>();

            if (users.Contains(user))
                users.Remove(user);

            users.Add(user);

            SaveUsersInFile(
                SerializeUsers(users)
                );
        }

        private string ReadFile()
        {
            return File
                .ReadAllText(_filePath);
        }

        private List<UserModel> DeserializeUsers()
        {
            var users = ReadFile() ?? "";
            return JsonConvert
                    .DeserializeObject<List<UserModel>>(users);
        }

        private string SerializeUsers(List<UserModel> users)
        {
            return JsonConvert
                    .SerializeObject(users, Formatting.Indented);
        }

        private void SaveUsersInFile(string serializedUsers)
        {
            File
                .WriteAllText(_filePath, serializedUsers);
        }

        private readonly string _filePath;
    }
}
