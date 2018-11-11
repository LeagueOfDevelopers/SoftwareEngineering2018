using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<IUser> Users => users;
        private List<IUser> users;

        public UserRepository(IEnumerable<IUser> users)
        {
            this.users = users.ToList() ?? throw new ArgumentNullException(nameof(users));
        }

        public UserRepository()
        {
            users = new List<IUser>();
        }

        public void AddUser(IUser user)
        {
            users.Add(user);
        }
        public bool Contains(Guid userId)
        {
            foreach(IUser user in users)
            {
                if (user.Id == userId) return true;
            }
            return false;
        }
    }
}
