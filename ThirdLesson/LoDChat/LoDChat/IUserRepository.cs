using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Users { get; } 
        IUser GetUser(Guid idOfUser);
        void SaveUser(IUser user);
    }
}
