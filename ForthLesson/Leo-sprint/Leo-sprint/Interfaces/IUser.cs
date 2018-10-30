using System;

namespace Leo_sprint
{
    public interface IUser
    {
        Guid _id { get; }
        string _nickname { get; }
    }
}
