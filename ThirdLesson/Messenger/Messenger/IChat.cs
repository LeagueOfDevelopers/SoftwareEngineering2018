using System;
using System.Collections.Generic;

namespace Messenger
{
    public interface IChat
    {
        Guid Id { get; }

        void AddMessage(IMessage message);

        void ChangeMessage(Guid messageId, string newBody);

        void DeleteMessage(Guid messageId);

        IMessage GetMessageById(Guid messageId);
    }
}
