using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Channel : Myrialog
    {
        public string Name { get; private set; }

        public Channel (string name, List<Message> messages, List<User> users, Dictionary<User, bool> admins, Guid id)
                :base(messages, users, admins, false, id)
        {
            Name = name;
        }

        public Channel (string name, User Master)
            : base(false, new Guid())
        {
            Users.Add(Master);
            Admins.Add(Master, true);
            Name = name;
        }
    }
}
