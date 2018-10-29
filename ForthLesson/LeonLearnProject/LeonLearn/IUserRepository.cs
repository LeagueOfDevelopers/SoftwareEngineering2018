using System;
using System.Collections;

namespace LeonLearn
{
    public interface IUserRepository
    {
        string Path { get; set; }

        User GetUser(Guid userId);
        void AddUser(User user);
        void DeleteUser(Guid userId);
        void EditUser(User editedUser);
        bool IsUser(Guid userId);
    }
}