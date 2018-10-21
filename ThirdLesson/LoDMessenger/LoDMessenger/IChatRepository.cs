using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public interface IChatRepository
    {
        IEnumerable<IChat> Chats { get; }
        IChat GetChat(Guid chatId);
        void SaveChat(IChat chat);
    }
}
