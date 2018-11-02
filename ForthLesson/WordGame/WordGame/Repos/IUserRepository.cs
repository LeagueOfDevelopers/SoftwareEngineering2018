using System;
using System.Collections.Generic;

namespace WordGame
{
    public interface IUserRepository
    {
        List<User> DeserializeUsers();
        User LoadUser(Guid userId);
        void SaveUser(User user);
        void UpdateUser(User user);
    }
}