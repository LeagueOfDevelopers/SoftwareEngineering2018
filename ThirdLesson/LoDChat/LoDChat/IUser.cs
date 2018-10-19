using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public interface IUser
    {
        string Name { get; }
        Guid Id { get; }
        List<IChat> Chats { get; }

        void SendMessage(Guid idOfChat, string message);
        void EditMessage(Guid idOfChat, Guid idOfMessage, string newMessage);
        void DeleteMessage(Guid idOfChat, Guid idOfMessage);
    }
}
