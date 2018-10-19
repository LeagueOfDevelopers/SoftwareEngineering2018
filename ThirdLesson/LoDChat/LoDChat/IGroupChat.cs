using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    interface IGroupChat
    {
        string NameOfChat { get; }
        List<IUser> Admins { get; }

        bool IsAdmin(List<IUser> admins, IUser user);
        bool IsOwnerOfMessage(Guid idOfChat, IUser user);
    }
}
