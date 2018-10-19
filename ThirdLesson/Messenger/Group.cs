using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Group : Myrialog
    {
        public string Name { get; private set; }

        public Group(string name, List<Message> messages, List<User> users, Dictionary<User, bool> admins, Guid id)
                : base(messages, users, admins, true, id)
        {
            Name = name;
        }

        public Group(string name, User Master)
            : base(true, new Guid())
        {
            Users.Add(Master);
            Admins.Add(Master, true);
            Name = name;
        }
    }
}
