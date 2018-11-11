using System;
using System.Collections.Generic;

namespace Chat
{
    public class GroupChat : IChat
    {
        private List<Message> messages;
        private List<IUser> users;
        private List<IUser> admins;

        public GroupChat(Guid id, List<Message> messages, List<IUser> users, List<IUser> admins)
        {
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.admins = admins ?? throw new ArgumentNullException(nameof(admins));
            Id = id;
        }

        public Guid Id { get; }

        public IEnumerable<Message> Messages => messages;
        public IEnumerable<IUser> Users => users;

        public bool AddMessage(Message message)
        {
            if (!admins.Contains(message.Sender)) throw new Exception("Try to send a message by not a Admin");
            messages.Add(message);
            return true;
        }

        public void DeleteMessage(Guid id, IUser sender)
        {
            if (!admins.Contains(sender)) throw new Exception("Try to delete a message by not a Admin");
            Message message = FindMessageById(id);
            if (message == null) return;
            messages.Remove(message);
        }

        public void EditMessage(Guid id, IUser sender, string newText)
        {
            if (!admins.Contains(sender)) throw new Exception("Try to edit a message by not a Admin");
            Message message = FindMessageById(id);
            if (message == null) return;
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
