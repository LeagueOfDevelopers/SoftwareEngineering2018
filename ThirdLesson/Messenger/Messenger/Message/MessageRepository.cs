using System;
using System.Collections.Generic;

namespace Messenger
{
    public class MessageRepository : IRepository<IMessage>
    {
        public MessageRepository(List<Message> messages)
        {
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        private List<Message> _messages { get; }

        public IEnumerable<IMessage> Items => _messages;

        public IMessage GetItem(Guid messageId)
        {
            return TryGetMessage(messageId) ?? throw new InvalidOperationException(
                $"Message with id {messageId} not found");
        }

        public void SaveItem(IMessage message)
        {
            if (TryGetMessage(message.Id) == null)
            {
                _messages.Add(message as Message);
            }
        }

        public void UpdateItem(IMessage message)
        {
            DeleteItemById(message.Id);
            SaveItem(message);
        }

        public void DeleteItem(IMessage message)
        {
            _messages.Remove(message as Message);
        }

        public void DeleteItemById(Guid messageId)
        {
            _messages.RemoveAll(message => message.Id == messageId);
        }

        public void AddItem(IMessage item)
        {
            _messages.Add(item as Message);
        }

        private IMessage TryGetMessage(Guid messageId)
        {
            foreach (var message in _messages)
            {
                if (message.Id == messageId)
                {
                    return message;
                }
            }
            return null;
        }
    }
}
