using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    interface IChannel
    {
        string NameOfChat { get; }
        IEnumerable<IUser> Admins { get; }

        bool IsAdmin(List<IUser> admins, IUser user);
    }
}
