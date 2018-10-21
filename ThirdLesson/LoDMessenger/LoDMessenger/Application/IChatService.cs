using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public interface IChatService
    {
        IEnumerable<IUser> GetChatParticipants(Guid chatId, Guid getterId);

        IEnumerable<IMessage> GetChatMessages(Guid chatId, Guid getterId);

        void SendMessage(Guid userId, Guid chatId, string massegeText);

        void EditMessage(Guid userId, Guid chatId, Guid messageId, string editedMassegeText);

        void RemoveMessage(Guid userId, Guid chatId, Guid messageId);

        void AddAdministratorToGroupChat(Guid uncheckedAdministratorId, Guid newAdministratorId, Guid chatId);

        void AddUserToGroupChat(Guid uncheckedAdministratorId, Guid newParticipant, Guid chatId);

        void AddAdministratorToChannel(Guid uncheckedAdministratorId, Guid newAdministratorId, Guid chatId);
    }
}
