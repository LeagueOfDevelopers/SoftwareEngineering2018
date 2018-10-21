using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public class Message
    {
        public Message(string userMessage, Guid idOfUser, Guid idOfMessage)
        {
            UserMessage = userMessage ?? throw new ArgumentNullException(nameof(userMessage));
            IdOfUser = idOfUser;
            IdOfMessage = idOfMessage;
        }
        
        public string UserMessage { get; private set; }
        public Guid IdOfUser { get; }
        public Guid IdOfMessage { get; }

        public void EditText(string newMessage)
        {
            UserMessage = newMessage;
        }
    }
}
