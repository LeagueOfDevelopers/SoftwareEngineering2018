using System;

namespace Messenger
{
    public class Message : IMessage
    {
        public Message(
            Guid id,
            Guid creatorId,
            string body)
        {
            Id = id;
            CreatorId = creatorId;
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public Guid Id { get; }
        public Guid CreatorId { get; }
        public string Body { get; private set; }

        public void Update(string newBody)
        {
            Body = newBody;
        }
    }
}
