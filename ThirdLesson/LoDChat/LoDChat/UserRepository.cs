using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(List<IUser> users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }
        public IEnumerable<IUser> Users => _users;
        public IUser GetUser (Guid idOfUser)
        {
            return TryGetUser(idOfUser) ??
                throw new InvalidOperationException(
                    $"User not found");
        }
        public void SaveUser(IUser user)
        {
            IUser existantUser = TryGetUser(user.Id);
            if(existantUser != null)
            {
                _users.Remove(existantUser);
            }
            _users.Add(user);
        }
        private IUser TryGetUser(Guid idOfUser)
        {
            foreach(var user in _users)
            {
                if(user.Id == idOfUser)
                {
                    return user;
                }
            }
            return null;
        }
        private List<IUser> _users;
    }
}
