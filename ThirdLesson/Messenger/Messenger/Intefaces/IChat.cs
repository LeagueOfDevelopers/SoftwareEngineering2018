using System;
using System.Collections.Generic;

namespace Messenger
{
    public interface IChat
    {
        Guid _id { get; }

        void ChangeMessage(string changed_message, Guid message_id, Guid user_id);
        void RemoveMessage(Guid message_id, Guid user_id);
        void SendMessage(IMessage message, Guid user_id);
        List<IMessage> GetMessages(IUser user);
        IMessage GetLastMessages(IUser user);

    }
}
