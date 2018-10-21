using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public class Channel : IChat
    {
        public Channel(
            Guid id, 
            IUser creator, 
            List<IUser> administrators,
            List<IUser> subscribers,
            List<IMessage> messages)
        {
            Id = id;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            _administrators = administrators ?? throw new ArgumentNullException(nameof(administrators));
            _subscribers = subscribers ?? throw new ArgumentNullException(nameof(subscribers));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public Guid Id { get; }

        public IUser Creator { get; }

        public IEnumerable<IUser> GetParticipants(IUser getter)
        {
            if (_subscribers.Contains(getter))
                return _subscribers;
            else
                throw new ArgumentException(
                    $"User with id {getter.Id} don't have permissions to get participants");
        }

        public IEnumerable<IMessage> GetMessages(IUser getter)
        {
            if (_subscribers.Contains(getter))
                return _messages;
            else
                throw new ArgumentException(
                    $"User with id {getter.Id} don't have permissions to get messages");
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

            if (_administrators.Contains(user))
                _messages.Remove(message);
            else
                throw new ArgumentException(
                    $"User with Id {user.Id} don't have permissions for delete this message");
        }

        public void SendMessage(IUser sender, IMessage message)
        {
            if (_administrators.Contains(sender))
                _messages.Add(message);
            else
                throw new ArgumentException(
                    $"User with Id {sender.Id} don't have permissions for send message in this channel");
        }

        public void AddAdministrator(IUser uncheckedAdministrator, IUser newAdministrator)
        {
            if (_administrators.Contains(uncheckedAdministrator))
                _administrators.Add(newAdministrator);
            else
                throw new ArgumentException(
                    $"User with Id {uncheckedAdministrator.Id} don't have permissions for add new administrator");
        }

        public void Subscribe(IUser subscriber)
        {
            _subscribers.Add(subscriber);
        }

        private IMessage FindMessageById(Guid messageId)
        {
            return _messages
                .Find(message =>
                    message.Id == messageId) ?? throw new ArgumentException(
                        $"message with id {messageId} not exist");
        }

        private List<IUser> _administrators;
        private List<IUser> _subscribers;
        private List<IMessage> _messages;
    }
}
