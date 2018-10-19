using System;
using System.Collections.Generic;

namespace Messenger
{
    public interface IUserService
    {
        void Identification(Guid user_id);

        void ChangeMessage(string changed_message, Guid message_id, IChat chat);
        void RemoveMessage(Guid message_id, IChat chat);
        void SendMessage(IMessage message, IChat chat);
        List<IMessage> GetMessages(IChat chat);
        IMessage GetLastMessages(IChat chat);
    }
}