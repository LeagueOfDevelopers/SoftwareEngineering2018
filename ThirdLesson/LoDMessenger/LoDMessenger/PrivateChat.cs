using System;
using System.Collections.Generic;
using System.Linq;

namespace LoDMessenger
{
    public class PrivateChat : IChat
    {
        public PrivateChat(
            Guid id, 
            IUser[] participants,
            List<IMessage> messages)
        {
            if (participants.Length == 2)
            {
                Id = id;
                _participants = participants;
                _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            }
            else
                throw new Exception("Incorrect participants size for private chat");
        }

        public Guid Id { get; }

        public IEnumerable<IMessage> GetMessages(IUser getter)
        {
            if (_participants.Contains(getter))
                return _messages;
            else
                throw new ArgumentException(
                    $"User with id {getter.Id} don't have permissions to get messages");
        }

        public IEnumerable<IUser> GetParticipants(IUser getter)
        {
            if (_participants.Contains(getter))
                return _participants;
            else
                throw new ArgumentException(
                    $"User with id {getter.Id} don't have permissions to get participants");
        }

        public void EditMessage(IUser editor, Guid messageId, string editedMessageText)
        {
            var message = FindMessageById(messageId);

            if (message.Author == editor)
                message.EditText(editedMessageText);
            else
                throw new ArgumentException(
                    $"User with Id {editor.Id} don't have permissions for edit this message");
        }

        public void RemoveMessage(IUser user, Guid messageId)
        {
            var message = FindMessageById(messageId);

            if (message.Author == user)
                _messages.Remove(message);
            else
                throw new ArgumentException(
                    $"User with Id {user.Id} don't have permissions for delete this message");
        }

        public void SendMessage(IUser sender, IMessage message)
        {
            if (_participants.Contains(sender))
                _messages.Add(message);
            else
                throw new ArgumentException(
                    $"User with Id {sender.Id} don't have permissions for send message in this channel");
        }

        private IMessage FindMessageById(Guid messageId)
        {
            return _messages
                .Find(message =>
                    message.Id == messageId) ?? throw new ArgumentException(
                        $"message with id {messageId} not exist");
        }

        private readonly IUser[] _participants;
        private List<IMessage> _messages;
    }
}
