using System;
using System.Collections.Generic;

namespace Messenger
{
    public class Group : IChat
    {
        public Group(
            Guid id,
            string name,
            UserRepository userRepository,
            MessageRepository messageRepository,
            UserRepository adminRepository)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            MessageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            AdminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            Messages = messageRepository.Items;
            Users = userRepository.Items;
            Admins = adminRepository.Items;
        }

        private MessageRepository MessageRepository { get; }
        private UserRepository UserRepository { get; }
        private UserRepository AdminRepository { get; }
        
        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<IMessage> Messages { get; }
        public IEnumerable<IUser> Users { get; }
        public IEnumerable<IUser> Admins { get; }

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

        public void AddUser(IUser user)
        {
            UserRepository.UpdateItem(user);
        }

        public void AddAdmin(IUser newAdmin)
        {
            AdminRepository.AddItem(newAdmin);
        }

        internal void RemoveAdmin(IUser oldAdmin)
        {
            AdminRepository.DeleteItem(oldAdmin);
        }
    }
}
