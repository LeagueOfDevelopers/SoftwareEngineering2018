using System;

namespace Messenger
{
    public class User : IUser
    {
        public User(Guid id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
