using System;

namespace LoD_Chat
{
    public interface IClient
    {

        Guid Id { get; }
        string Name { get; }
    }
}
