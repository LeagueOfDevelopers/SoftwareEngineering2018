using System.Collections.Generic;

namespace Chat
{
    public interface IChatRepository
    {
        IEnumerable<IChat> Chats { get; }

        void AddChat(IChat chat);
    }
}
