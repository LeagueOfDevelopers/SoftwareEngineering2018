using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; }

        public User (Guid id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}
