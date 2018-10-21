using System;
using System.Collections.Generic;

namespace Chat
{
    public class ChanelChat : IChat
    {
        private List<Message> messages;
        private List<IUser> users;
        private IUser admin;

        public ChanelChat(Guid id, List<Message> messages, List<IUser> users, IUser admin)
        {
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.admin = admin ?? throw new ArgumentNullException(nameof(admin));
            Id = id;
        }

        public Guid Id { get; }
        public IEnumerable<Message> Messages => messages;
        public IEnumerable<IUser> Users => users;

        public bool AddMessage(Message message)
        {
            if (!admin.Equals(message.Sender)) throw new Exception("Try to send a message by not a Admin");
            messages.Add(message);
            return true;
        }

        public void DeleteMessage(Guid id, IUser sender)
        {
            Message message = FindMessageById(id);
            if (message == null) return;
            if (!admin.Equals(message.Sender)) throw new Exception("Try to delete a message by not a Admin");

            messages.Remove(message);
        }

        public void EditMessage(Guid id, IUser sender, string newText)
        {
            Message message = FindMessageById(id);
            if (message == null) return;
            if (!admin.Equals(message.Sender)) throw new Exception("Try to edit a message by not a Admin");

            message.Text = newText;
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
    }
}
