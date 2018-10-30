using System;
using System.Collections.Generic;

namespace LoD_Chat
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly List<IMessage> _messages;

        public IMessage[] Messages => _messages.ToArray();

        public IMessage GetMessage(Guid messageId)
        {
            return TryGetMessage(messageId) ?? throw new InvalidOperationException(
                $"Message with id {messageId} not found");
        }

        public void DeleteMessage(IMessage message)
        {
            IMessage existantMessage = TryGetMessage(message.Id);

            if (existantMessage != null)
            {
                _messages.Remove(existantMessage);
            }

            _messages.Remove(message);
        }

        public IMessage FindMessage(IMessage message){

            return _messages.Find(item => item.Id == message.Id);
        }

        public void AddMessage(IMessage message)
        {
            IMessage existantMessage = TryGetMessage(message.Id);

            if (existantMessage != null)
            {
                _messages.Remove(existantMessage);
            }

            _messages.Add(message);
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
