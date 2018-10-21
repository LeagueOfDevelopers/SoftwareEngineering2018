using System;
using System.Collections.Generic;

namespace Messenger
{
    public class UserRepository : IRepository<IUser>
    {
        Dictionary<Guid, IUser> users;

        public UserRepository(Dictionary<Guid, IUser> users)
        {
            this.users = users;
        }

        public void Create(IUser user)
        {
            users.Add(user._id, user);            
        }

        public IUser Get(Guid id)
        {
            if (users.TryGetValue(id, out IUser user))
            {
                return user;
            }
            throw new InvalidOperationException($"User with id {id} not found");
        }

        public void Remove(Guid id)
        {
            if (users.ContainsKey(id))
            {
                users.Remove(id);
            }
            throw new InvalidOperationException($"User with id {id} already removed");
        }        

        public void Save(IUser user)
        {
            if (users.TryGetValue(user._id, out IUser existantUser))
            {
                users.Remove(user._id);
            }

            users.Add(user._id, user);
        }
    }
}
