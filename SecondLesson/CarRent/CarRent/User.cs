using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class User
    {
        public User(string name, int id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id;
        }

        string Name { get; }
        int Id { get; }
    }
}
