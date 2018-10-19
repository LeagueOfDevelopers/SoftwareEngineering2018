using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class User
    {
        public string Name { get; }
        public Guid Id { get; }
        
        public User(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }
}
