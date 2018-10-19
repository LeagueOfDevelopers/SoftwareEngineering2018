using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public class GroupChat : IChat
    {
        public GroupChat(
            Guid id, 
            IUser creator, 
            List<IUser> administrators, 
            List<IUser> participants,
            List<IMessage> messages)
        {
            Id = id;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            _administrators = administrators ?? throw new ArgumentNullException(nameof(administrators));
            _participants = participants ?? throw new ArgumentNullException(nameof(participants));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public Guid Id { get; }

        public IUser Creator { get; }

        public IEnumerable<IUser> GetParticipants(IUser getter)
        {
            if (_participants.Contains(getter))
                return _participants;
            else
                throw new ArgumentException(
                    $"User with id {getter.Id} don't have permissions to get participants");
        }

        public IEnumerable<IMessage> GetMessages(IUser getter)
        {
            if (_participants.Contains(getter))
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
            if (_participants.Contains(sender))
                _messages.Add(message);
            else
                throw new ArgumentException(
                    $"User with Id {sender.Id} don't have permissions for send message in this channel");
        }

        public void AddParticipant(IUser uncheckedAdministrator, IUser newParticipant)
        {
            if (_administrators.Contains(uncheckedAdministrator))
                _participants.Add(newParticipant);
            else
                throw new ArgumentException(
                    $"User with Id {uncheckedAdministrator.Id} don't have permissions for add new participant");
        }

        public void AddAdministrator(IUser uncheckedAdministrator, IUser newAdministrator)
        {
            if (_administrators.Contains(uncheckedAdministrator))
                _administrators.Add(newAdministrator);
            else
                throw new ArgumentException(
                    $"User with Id {uncheckedAdministrator.Id} don't have permissions for add new administrator");
        }

        private IMessage FindMessageById(Guid messageId)
        {
            return _messages
                .Find(message =>
                    message.Id == messageId) ?? throw new ArgumentException(
                        $"message with id {messageId} not exist");
        }

        private List<IUser> _administrators;
        private List<IUser> _participants;
        private List<IMessage> _messages;
    }
}
