using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public class ChatService : IChatService
    {
        public IEnumerable<IUser> GetChatParticipants(Guid chatId, Guid getterId)
        {
            var chat = _chatRepository.GetChat(chatId);
            var getter = _userRepository.GetUser(getterId);

            return chat.GetParticipants(getter);
        }

        public IEnumerable<IMessage> GetChatMessages(Guid chatId, Guid getterId)
        {
            var chat = _chatRepository.GetChat(chatId);
            var getter = _userRepository.GetUser(getterId);

            return chat.GetMessages(getter);
        }

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void SendMessage(Guid senderId, Guid chatId, string messageText)
        {
            var sender = _userRepository.GetUser(senderId);
            var chat = _chatRepository.GetChat(chatId);
            var message = new Message(
                Guid.NewGuid(),
                sender,
                messageText);

            chat.SendMessage(sender, message);
            _chatRepository
                .SaveChat(chat);
            _userRepository.
                SaveUser(sender);
        }

        public void EditMessage(Guid editorId, Guid chatId, Guid messageId, string editedMassegeText)
        {
            var editor = _userRepository.GetUser(editorId);
            var chat = _chatRepository.GetChat(chatId);

            chat.EditMessage(editor, messageId, editedMassegeText);
            _chatRepository
                .SaveChat(chat);
            _userRepository
                .SaveUser(editor);
        }

        public void RemoveMessage(Guid userId, Guid chatId, Guid messageId)
        {
            var user = _userRepository.GetUser(userId);
            var chat = _chatRepository.GetChat(chatId);

            chat.RemoveMessage(user, messageId);
            _chatRepository
                .SaveChat(chat);
            _userRepository
                .SaveUser(user);
        }

        public void AddAdministratorToGroupChat(Guid uncheckedAdministratorId, Guid newAdministratorId, Guid chatId)
        {
            var chat = _chatRepository.GetChat(chatId);
            var newAdministrator = _userRepository.GetUser(newAdministratorId);
            var uncheckedAdministrator = _userRepository.GetUser(uncheckedAdministratorId);

            if (chat is GroupChat groupChat)
                groupChat
                    .AddAdministrator(uncheckedAdministrator, newAdministrator);
            else
                throw new ArgumentException(
                    $"Chat with id {chatId} not a group chat");
            _chatRepository.SaveChat(groupChat);
            _userRepository.SaveUser(uncheckedAdministrator);
            _userRepository.SaveUser(newAdministrator);
        }

        public void AddUserToGroupChat(Guid uncheckedAdministratorId, Guid newParticipantId, Guid chatId)
        {
            var chat = _chatRepository.GetChat(chatId);
            var uncheckedAdministrator = _userRepository.GetUser(uncheckedAdministratorId);
            var newParticipant = _userRepository.GetUser(newParticipantId);

            if (chat is GroupChat groupChat)
                groupChat
                    .AddParticipant(uncheckedAdministrator, newParticipant);
            else
                throw new ArgumentException(
                    $"Chat with id {chatId} not a group chat");
            _chatRepository.SaveChat(groupChat);
            _userRepository.SaveUser(newParticipant);
            _userRepository.SaveUser(uncheckedAdministrator);
        }

        public void AddAdministratorToChannel(Guid uncheckedAdministratorId, Guid newAdministratorId, Guid chatId)
        {
            var chat = _chatRepository.GetChat(chatId);
            var newAdministrator = _userRepository.GetUser(newAdministratorId);
            var uncheckedAdministrator = _userRepository.GetUser(uncheckedAdministratorId);

            if (chat is Channel channel)
                channel
                    .AddAdministrator(uncheckedAdministrator, newAdministrator);
            else
                throw new ArgumentException(
                    $"Chat with id {chatId} not a channel");
            _chatRepository.SaveChat(channel);
            _userRepository.SaveUser(uncheckedAdministrator);
            _userRepository.SaveUser(newAdministrator);
        }

        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
    }
}
