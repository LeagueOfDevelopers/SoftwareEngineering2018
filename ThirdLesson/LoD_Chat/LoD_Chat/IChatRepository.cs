using System;
namespace LoD_Chat
{
    public interface IChatRepository
    {
        IChat[] Chats { get; }

        IChat GetChat(Guid chatId);

        void AddChat(IChat chat);
    }
}
