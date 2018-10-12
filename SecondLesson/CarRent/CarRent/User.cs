using System;

namespace CarRent
{
    public class User
    {
        public Guid Id;
        public string Name;

        public User(string name)
        {
            Name = name;
            Id = new Guid();
        }
    }
}
