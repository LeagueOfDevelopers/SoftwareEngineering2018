using System;
using System.Collections.Generic;

namespace LoD_Chat
{
    public class Client : IClient
    {
        public Client(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
