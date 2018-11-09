using MongoDB.Driver;
using System;

namespace Leo_sprint
{
    public interface IUserRepository
    {
        User LaodUser(Guid id);
        void SaveUser(User user);
        Guid CreateUser(string nickname);
    }
}