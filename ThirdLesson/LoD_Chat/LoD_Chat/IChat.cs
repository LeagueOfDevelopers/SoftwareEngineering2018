using System;
using System.Collections.Generic;

namespace LoD_Chat
{
    public interface IChat
    {
        Guid Id { get; }
        Client Creator { get; }
        string ChatName { get; set; }
        ClientRepository Members { get; }
        MessagesRepository Messages { get; }

        void AddMessage(Message message);

        void EditMessage(Message message, string textChanges);

        void DeleteMessage(Message message);

    }
}
