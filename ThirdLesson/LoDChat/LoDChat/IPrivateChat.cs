using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public interface IPrivateChat
    {
        bool IsOwnerOfMessage(Guid idOfMessage, IUser user);
    }
}
