using System;

namespace Chat
{
    public class Message
    {
        public Message(Guid id, IUser sender, string text)
        {
            Id = id;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public Guid Id { get; }
        public IUser Sender { get; }
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            Message message = (Message)obj;
            return (Id == message.Id) && (Sender.Id == message.Sender.Id) && (Text == message.Text);
        }
    }
}
