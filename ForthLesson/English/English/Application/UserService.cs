using System;
using System.Collections.Generic;
using English.Domain;
using English.Infrastructure;

namespace English.Application
{
    public class UserService : IUserService
    {
        private readonly UserDatabase _userDatabase = UserDatabase.GetInstance();

        public UserService()
        {
        }

        public Guid RegisterNewUser(string name)
        {
            var userId = Guid.NewGuid();
            var user = new User(userId, name, new Dictionary<Word, int>(), new Dictionary<Word, int>());
            _userDatabase.SaveUserToFile(user);
            return userId;
        }

        public IUser LoadUser(Guid userId)
        {
            return _userDatabase.LoadUserFromFile(userId);
        }
    }
}
