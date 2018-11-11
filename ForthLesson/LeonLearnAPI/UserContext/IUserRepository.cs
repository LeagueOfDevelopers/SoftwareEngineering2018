using System;

namespace UserContext
{
    public interface IUserRepository
    {
        User GetUser(Guid userId);
        void AddUser(User user);
        void DeleteUser(Guid userId);
        void EditUser(User editedUser);
        bool IsUser(Guid userId);
    }
}