using System;
using System.Collections.Generic;

namespace Messenger
{
    public interface IChat
    {
        Guid Id { get; }

        IEnumerable<IUser> Users { get; }

        IEnumerable<IMessage> Messages { get; }

        void AddMessage(IMessage message);

        void ChangeMessage(Guid messageId, string newBody);

        void DeleteMessage(Guid messageId);

        IMessage GetMessageById(Guid messageId);
    }
}
