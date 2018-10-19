using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Chat : Myrialog
    {
        public Chat(List<Message> messages, List<User> users, Dictionary<User, bool> admins, Guid id)
                : base(messages, users, admins, true, id)
        {

        }

        public Chat(User first, User second)
            : base(true, new Guid())
        {
            Users.Add(first);
            Admins.Add(first, false);
            Users.Add(second);
            Admins.Add(second, false);
        }
    }
}
