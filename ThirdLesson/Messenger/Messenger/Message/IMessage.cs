using System;

namespace Messenger
{
    public interface IMessage
    {
        Guid Id { get; }

        Guid CreatorId { get; }

        string Body { get; }

        void Update(string newBody);
    }
}
