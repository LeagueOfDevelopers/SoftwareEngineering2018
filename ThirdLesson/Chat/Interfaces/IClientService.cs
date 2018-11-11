using System;

namespace Chat
{
    public interface IClientService
    {
        void AddMessage(Guid chatId, Guid userId, string text);
        void DeleteMessage(Guid chatId, Guid userID, Guid messageId);
        void EditMessage(Guid chatId, Guid userId, Guid messageId, string newText);
        void CreateChat(Guid userId, IChat chat);
    }
}
