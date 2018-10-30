using System;
using System.IO;

namespace LoDSprint.Repositories
{
    public class InFileUsersRepository : IInFileUsersRepository
    {
        public InFileUsersRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public IUser LoadUser(Guid userId)
        {
            return ReadFile()
                .DeserializeUsers()
                [userId];
        }

        public void SaveUser(IUser user)
        {
            var users = ReadFile()
                .DeserializeUsers();

            users[user.Id] = user;

            SaveUsersInFile(
                users
                .SerializeUsers()
                );
        }

        private string ReadFile()
        {
            return File
                .ReadAllText(_filePath);
        }

        private void SaveUsersInFile(string serializedUser)
        {
            File
                .WriteAllText(_filePath, serializedUser);
        }

        private readonly string _filePath;
    }
}
