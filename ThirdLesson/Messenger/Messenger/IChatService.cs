using System;

namespace Messenger
{
    public interface IChatService
    {
        Guid Id { get; }

        void CreateChat(IChat chat);

        void AddMessage(Guid chatId, Guid userId, IMessage message);

        void ChangeMessage(Guid chatId, Guid userId, Guid messageId, string newBody);

        void DeleteMessage(Guid chatId, Guid userId, Guid messageId);
    }
}
