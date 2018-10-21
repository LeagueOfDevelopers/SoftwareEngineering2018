using System;
using System.Collections.Generic;

namespace Chat
{
    public class PrivateChat : IChat
    {
        private List<Message> messages;
        private List<IUser> users;

        public PrivateChat(Guid id, List<Message> messages, List<IUser> users)
        {
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            Id = id;
        }

        public Guid Id { get; }
        public IEnumerable<Message> Messages => messages;
        public IEnumerable<IUser> Users => users;

        public bool AddMessage(Message message)
        {
            if (!users.Contains(message.Sender)) throw new Exception("Not a member of Chat");
            messages.Add(message);
            return true;
        }

        public void DeleteMessage(Guid id, IUser sender)
        {
            if (!users.Contains(sender)) throw new Exception("Not a member of Chat");

            messages.Remove(FindMessageById(id));
        }

        public void EditMessage(Guid id, IUser sender, string newText)
        {
            if (!users.Contains(sender)) throw new Exception("Not a member of Chat");

            Message message = FindMessageById(id);
            if (message == null) return;
            if (sender.Equals(message.Sender))
            {
                message.Text = newText;
            }
        }

        public Message FindMessageById(Guid id)
        {
            foreach (Message message in messages)
            {
                if (message.Id == id)
                {
                    return message;
                }
            }
            return null;
        }

        public IUser FindUserById(Guid id)
        {
            foreach (IUser user in users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
