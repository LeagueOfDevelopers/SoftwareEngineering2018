using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(List<IUser> users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public IUser[] Users => _users.ToArray();

        public IUser GetUser(Guid userId)
        {
            return TryGetUser(userId)
                ?? throw new ArgumentException(
                    $"User with id {userId} not exist");
        }

        public void SaveUser(IUser user)
        {
            var existentUser = TryGetUser(user.Id);

            if (existentUser != null)
                _users
                    .Remove(existentUser);

            _users.Add(user);
        }

        private IUser TryGetUser(Guid userId)
        {
            return _users
                .Find(user =>
                    user.Id == userId);
        }

        private List<IUser> _users;
    }
}
