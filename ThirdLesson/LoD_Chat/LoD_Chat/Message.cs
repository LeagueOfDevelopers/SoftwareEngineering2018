using System;
namespace LoD_Chat
{
    public class Message : IMessage
    {
        public Message(Guid id, IClient sender, DateTimeOffset sendTime, string text)
        {
            Id = id;
            Sender = sender;
            SendTime = sendTime;
            Text = text;
        }

        public void EditMessage(string edittedtext)
        {
            Text = edittedtext;
        }

        public Guid Id { get; }
        public IClient Sender { get; }
        public DateTimeOffset SendTime { get; }
        public string Text { get; private set; }
    }
}
