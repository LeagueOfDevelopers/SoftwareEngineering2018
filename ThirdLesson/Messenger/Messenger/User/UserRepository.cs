using System;
using System.Collections.Generic;

namespace Messenger
{
    public class UserRepository : IRepository<IUser>
    {
        private List<User> _users { get; set; }

        public UserRepository(List<User> users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public IEnumerable<IUser> Items => _users;

        public IUser GetItem(Guid userId)
        {
            return TryGetUser(userId) ?? throw new InvalidOperationException(
                $"User with id {userId} not found");
        }

        public void SaveItem(IUser user)
        {
            if (TryGetUser(user.Id) == null)
            {
                _users.Add(user as User);
            }
        }

        public void UpdateItem(IUser user)
        {
            DeleteItemById(user.Id);
            SaveItem(user);
        }

        public void DeleteItem(IUser user)
        {
            _users.Remove(user as User);
        }

        public void DeleteItemById(Guid userId)
        {
            _users.RemoveAll(user => user.Id == userId);
        }

        public void AddItem(IUser item)
        {
            _users.Add(item as User);
        }

        private IUser TryGetUser(Guid userId)
        {
            foreach (var client in _users)
            {
                if (client.Id == userId)
                {
                    return client;
                }
            }
            return null;
        }
    }
}
