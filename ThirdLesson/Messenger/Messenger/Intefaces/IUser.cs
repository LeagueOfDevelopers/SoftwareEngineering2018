using System;

namespace Messenger
{
    public interface IUser
    {
        Guid _id { get; }
        string nickname { get; }
    }
}
