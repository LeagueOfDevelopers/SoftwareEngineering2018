using System;
using System.Collections.Generic;

namespace Chat
{
    public interface IChat
    {
        Guid Id { get; }
        IEnumerable<Message> Messages { get; }
        IEnumerable<IUser> Users { get; }

        bool AddMessage(Message message);
        void EditMessage(Guid id, IUser sender, string newText);
        void DeleteMessage(Guid id, IUser sender);
        Message FindMessageById(Guid id);
        IUser FindUserById(Guid id);
    }
}
