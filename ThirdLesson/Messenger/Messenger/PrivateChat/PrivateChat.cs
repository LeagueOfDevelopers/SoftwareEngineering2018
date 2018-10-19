using System;
using System.Linq;
using System.Collections.Generic;

namespace Messenger
{
    public class PrivateChat : IChat
    {
        public PrivateChat(
            Guid id,
            UserRepository userRepository,
            MessageRepository messageRepository)
        {
            Id = id;
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            MessageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));

            if (userRepository.Items.Count() != 2)
            {
                throw new Exception($"Private chat has only two members, not {userRepository.Items.Count()}");
            }

            Messages = messageRepository.Items;
            Users = userRepository.Items;
        }

        private UserRepository UserRepository { get; }
        private MessageRepository MessageRepository { get; }

        public Guid Id { get; }
        public IEnumerable<IMessage> Messages { get; }
        public IEnumerable<IUser> Users { get; }
        public IUser FirstUser => Users.ElementAt(0);
        public IUser SecondUser => Users.ElementAt(1);

        public void AddMessage(IMessage message)
        {
            MessageRepository.SaveItem(message);
        }

        public void ChangeMessage(Guid messageId, string newBody)
        {
            var message = MessageRepository.GetItem(messageId);
            message.Update(newBody);
        }

        public void DeleteMessage(Guid messageId)
        {
            MessageRepository.DeleteItemById(messageId);
        }

        public IMessage GetMessageById(Guid messageId)
        {
            return MessageRepository.GetItem(messageId);
        }
    }
}
