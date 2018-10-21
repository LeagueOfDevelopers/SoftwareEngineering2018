using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public class PrivateChat : IChat, IPrivateChat
    {
        public PrivateChat(Guid idOfChat, List<Message> messages, List<IUser> users)
        {
            IdOfChat = idOfChat;
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public Guid IdOfChat { get; }

        public IEnumerable<Message> Messages => _messages;

        public IEnumerable<IUser> Users => _users;

        public bool IsBeInChat(IUser user)
        {
            foreach (var chat in user.Chats)
            {
                if (chat.IdOfChat.Equals(IdOfChat)){
                    return true;
                }
            }
            return false;
        }

        public bool IsOwnerOfMessage(Guid idOfUser,IUser user)
        {
           if(idOfUser == user.Id)
            {
                return true;
            }
            return false;
        }

        public void AddMessage(IUser user, string message)
        {
            Message newMessage = new Message(message, user.Id, Guid.NewGuid());
            _messages.Add(newMessage);
        }

        public void DeleteMessage(IUser user, Guid idOfMessage)
        {
            Message currentMessage = _messages.Find(mes => mes.IdOfMessage == idOfMessage) ?? throw new ArgumentException($"message with id {idOfMessage} not found");
            if (IsBeInChat(user) && IsOwnerOfMessage(currentMessage.IdOfUser,user))
            {
                Message removedMessage = _messages.Find(message => message.IdOfMessage == idOfMessage);
                _messages.Remove(removedMessage);
            }
        }

        public void EditMessage(IUser user, Guid idOfMessage, string newMessage)
        {
            Message currentMessage = _messages.Find(mes => mes.IdOfMessage == idOfMessage) ?? throw new ArgumentException($"message with id {idOfMessage} not found");
            if (IsBeInChat(user) && IsOwnerOfMessage(currentMessage.IdOfUser,user))
            {
                Message editMessage = _messages.Find(message => message.IdOfMessage == idOfMessage);
                editMessage.EditText(newMessage);
            }
        }

        private List<Message> _messages;
        private List<IUser> _users;
    }
}
