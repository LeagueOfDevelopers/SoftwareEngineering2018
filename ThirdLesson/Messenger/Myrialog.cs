using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Myrialog
    {
        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
        public Dictionary<User, bool> Admins { get; set; }
        public bool AbleToAdd { get; }
        public Guid Id { get; }

        public bool AddUser(User Master, User ToAdd)
        {
            if (!Admins[Master]) return false;
                else
                {
                Users.Add(ToAdd);
                Admins.Add(ToAdd, false);
                return true;
                }
        }

        public bool AddAdmin(User Master, User ToAdd)
        {
            if ((Admins[Master]) && (Users.Contains(ToAdd))){
                Admins[ToAdd] = true;
                return true;
            }else
            return false;
        }

        public bool AddMesage(User Master, Message ToAdd)
        {
            if ((Admins[Master]) || (AbleToAdd))
            {
                Messages.Add(ToAdd);
                return true;
            }
            else return false;
        }

        public bool DeleteMessage(User Master, Guid MessageId)
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                if ((Messages[i].Id == MessageId) && ((Admins[Master]) || (Messages[i].Author == Master)))
                {
                    Messages.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool EditMessage(User Master, Guid MessageId, string NewText)
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                if ((Messages[i].Id == MessageId) && (Messages[i].Author == Master))
                {
                    Messages[i].Text = NewText;
                    return true;
                }
            }

            return false;
        }

        public Myrialog(List<Message> messages, List<User> users, Dictionary<User, bool> admins, bool ableToAdd, Guid id)
        {
            Messages = messages;
            Users = users;
            Admins = admins;
            AbleToAdd = ableToAdd;
            Id = id;
        }

        public Myrialog(bool ableToAdd, Guid id)
        {
            Messages = new List<Message>();
            Users = new List<User>();
            Admins = new Dictionary<User, bool>();
            AbleToAdd = ableToAdd;
            Id = id;
        }
    }
}
