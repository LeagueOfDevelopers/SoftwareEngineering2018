using System;
using System.Linq;

namespace Messenger
{
    public class PrivateChatService : IChatService
    {
        public PrivateChatService(Guid id, PrivateChatRepository privateChatRepository)
        {
            Id = id;
            _privateChatRepository = privateChatRepository
                ?? throw new ArgumentNullException(nameof(privateChatRepository));
        }

        private PrivateChatRepository _privateChatRepository { get; }

        public Guid Id { get; }

        public void AddMessage(Guid privateChatId, Guid userId, IMessage message)
        {
            var privateChat = _privateChatRepository.GetItem(privateChatId);

            if (!CanSendMessage(privateChatId, userId))
            {
                throw new ArgumentException($"User {userId} can't send messege to foreign private chat");
            }

            if (privateChat.Messages.Contains(message))
            {
                throw new MemberAccessException($"Message with id {message.Id} already exists");
            }

            privateChat.AddMessage(message);
        }

        public void ChangeMessage(Guid privateChatId, Guid userId, Guid messageId, string newBody)
        {
            var privateChat = _privateChatRepository.GetItem(privateChatId);
            var message = privateChat.GetMessageById(messageId);

            if (!HasAccessToMessage(userId, message))
            {
                throw new MemberAccessException($"User {userId} can't change foreign message ${message.Id}");
            }

            privateChat.ChangeMessage(messageId, newBody);
        }

        public void DeleteMessage(Guid privateChatId, Guid userId, Guid messageId)
        {
            var privateChat = _privateChatRepository.GetItem(privateChatId);
            var message = privateChat.GetMessageById(messageId);

            if (!HasAccessToMessage(userId, message))
            {
                throw new MemberAccessException($"User {userId} can't delete foreign message ${message.Id}");
            }

            privateChat.DeleteMessage(messageId);
        }

        public void CreateChat(IChat privateChat)
        {
            _privateChatRepository.SaveItem(privateChat as PrivateChat);
        }

        private bool HasAccessToMessage(Guid userId, IMessage message)
        {
            return message.CreatorId == userId;
        }

        private bool CanSendMessage(Guid privateChatId, Guid userId)
        {
            var privateChat = _privateChatRepository.GetItem(privateChatId);
            return (userId == privateChat.FirstUser.Id) || (userId == privateChat.SecondUser.Id);
        }
    }
}
