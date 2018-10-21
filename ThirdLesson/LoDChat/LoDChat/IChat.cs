using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public interface IChat
    {
        Guid IdOfChat { get; }
        IEnumerable<Message> Messages { get; }
        IEnumerable<IUser> Users { get; }

        bool IsBeInChat(IUser user);
        void AddMessage(IUser user, string message);
        void DeleteMessage(IUser user, Guid idOfMessage);
        void EditMessage(IUser user,  Guid idOfMessage, string newMessage);
    }
}
