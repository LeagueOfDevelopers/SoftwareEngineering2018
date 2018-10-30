using System;
namespace LoD_Chat
{
    public interface IMessagesRepository
    {
        IMessage[] Messages { get; }

        IMessage GetMessage(Guid messageId);

        void AddMessage(IMessage message);
    }
}
