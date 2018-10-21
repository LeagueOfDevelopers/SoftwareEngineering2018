using System;

namespace LoDMessenger
{
    public class Message : IMessage
    {
        public Message(
            Guid id, 
            IUser author, 
            string messageText)
        {
            Id = id;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
        }

        public Guid Id { get; }

        public IUser Author { get; }

        public string MessageText { get; private set; }

        public void EditText(string editedMessageText)
        {
            MessageText = editedMessageText;
        }
    }
}
