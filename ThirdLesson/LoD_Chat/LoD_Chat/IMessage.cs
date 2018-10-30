using System;
namespace LoD_Chat
{
    public interface IMessage
    {
        void EditMessage(string edittedtext);

        Guid Id { get; }
        IClient Sender { get; }
        DateTimeOffset SendTime { get; }
        string Text { get; }
    }
}
