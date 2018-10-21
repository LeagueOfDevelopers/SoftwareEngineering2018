using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public interface IChat
    {
        Guid Id { get; }

        IEnumerable<IUser> GetParticipants(IUser getter);

        IEnumerable<IMessage> GetMessages(IUser getter);

        void SendMessage(IUser sender, IMessage message);

        void RemoveMessage(IUser user, Guid messageId);

        void EditMessage(IUser editor, Guid messageId, string editedMessageText);
    }
}
