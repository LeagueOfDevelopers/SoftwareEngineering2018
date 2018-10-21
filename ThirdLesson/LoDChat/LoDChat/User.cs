using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public class User : IUser
    {
        public User(string name, Guid id, List<IChat> chats)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id;
            Chats = chats ?? throw new ArgumentNullException(nameof(chats));
        }

        public string Name { get; }

        public Guid Id { get; }

        public List<IChat> Chats { get; set; }

        public void DeleteMessage(Guid idOfChat, Guid idOfMessage)
        {
            throw new NotImplementedException();
        }

        public void EditMessage(Guid idOfChat, Guid idOfMessage, string newMessage)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Guid idOfChat, string message)
        {
            throw new NotImplementedException();
        }

    }
}
