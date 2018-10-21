using System;

namespace Messenger
{
    public interface IMessage
    {
        Guid _id { get; }
        IUser _author { get; }
        DateTimeOffset _departure_time { get; }
    }
}
