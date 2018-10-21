using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat.Application
{
    interface IUserService
    {
        void SendNewMessage(IChat chat, IUser user, string message);
        void DeleteMessage(Guid idOfChat, Guid idOfMessage, Guid idOfUser);
        void EditMessage(Guid idOfChat, Guid idOfMessage, string NewMessage, Guid idOfUser);
        void AddAdmin(Guid idOfUser, Guid idOfChat);
        void CreatePrivateChat(IUser interlocutor, IUser user);
        void CreateGroupChat(List<IUser> users, IUser user);
        void CreateChannel(List<IUser> users, IUser user);
    }
}
