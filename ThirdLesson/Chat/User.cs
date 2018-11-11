using System;

namespace Chat
{
    public class User : IUser
    {
        

        public User(Guid id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public Guid Id { get; }
        public string Name { get; }

        public Message SendMessage(string text)
        {
            return new Message(Guid.NewGuid(), this, text);
        }
        public override bool Equals(object obj)
        {
            User user = (User)obj;
            return (user.Id == Id) && (user.Name == Name);
        }
    }
}
