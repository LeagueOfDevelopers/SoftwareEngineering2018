using System;
using System.Collections.Generic;
using System.Linq;

namespace Messenger
{
    public class Channel : IChat
    {
        public Guid _id { get; }
        public string _name { private set; get; } //not null

        private Dictionary<Guid, IUser> admins;
        private Dictionary<Guid, IUser> followers;
        private Dictionary<Guid, IMessage> messages;

        public Channel(Guid id, string name, Dictionary<Guid, IUser> admins, Dictionary<Guid, IUser> followers, Dictionary<Guid, IMessage> messages)
        {
            _id = id;
            _name = name ?? throw new ArgumentNullException(nameof(name));
            this.admins = admins ?? throw new ArgumentNullException(nameof(admins));//не пустая коллекция
            this.followers = followers;
            this.messages = messages;
        }

        public void AddNewAdmin(IUser new_admin, Guid old_admin_id)
        {
            if (admins.ContainsKey(old_admin_id))
            {
                admins.Add(new_admin._id, new_admin);
                followers.Add(new_admin._id, new_admin);
            }
            else throw new InvalidOperationException($"You are not admin");
        }


        public void ChangeName(string new_name, Guid user_id)
        {
            if (admins.ContainsKey(user_id))
            {
                _name = new_name;
            }
            else throw new InvalidOperationException($"You are not admin");
        }

        public void ChangeMessage(string changed_message, Guid message_id, Guid user_id)
        {
            if (messages.TryGetValue(message_id, out IMessage finded_message) && followers.TryGetValue(user_id, out IUser finded_user))
            {
                if (finded_message._author == finded_user)
                {
                    messages[message_id] = new Message(changed_message, finded_message._id, finded_message._author, finded_message._departure_time);
                }
                else throw new InvalidOperationException($"This message not your");
            }
            else throw new InvalidOperationException($"Message or user does not exist");

        }

        public void RemoveMessage(Guid message_id, Guid user_id)
        {
            if (followers.ContainsKey(user_id) && messages.ContainsKey(message_id))
            {
                if (admins.ContainsKey(user_id))
                {
                    messages.Remove(message_id);
                }
                else throw new InvalidOperationException($"You are not admin");
            }
            else throw new InvalidOperationException($"Message or user does not exist");

        }

        public void SendMessage(IMessage message, Guid user_id)
        {
            if (followers.ContainsKey(user_id))
            {
                messages.Add(message._id, message);
            }
            else throw new InvalidOperationException($"Firstly join the channel");
        }

        public void Join(IUser follower)
        {
            followers.Add(follower._id, follower);
        }

        public List<IMessage> GetMessages(IUser user)
        {
            if (followers.ContainsKey(user._id))
            {
                return messages.Values.ToList();
            }
            throw new InvalidOperationException($"Firstly join the channel");

        }

        public IMessage GetLastMessages(IUser user)
        {
            var min_time = DateTimeOffset.MinValue;
            var empty_user = new User(string.Empty, Guid.Empty);
            var last_message = new Message(string.Empty, Guid.Empty, empty_user, min_time);

            if (followers.ContainsKey(user._id))
            {
                foreach (Message message in messages.Values)
                {
                    if (min_time > message._departure_time)
                    {
                        min_time = message._departure_time;
                        last_message = message;
                    }

                }
                return last_message;
            }
            throw new InvalidOperationException($"Firstly join the channel");
        }

        public override bool Equals(object obj)
        {
            var channel = obj as Channel;
            return channel != null &&
                   _id.Equals(channel._id) &&
                   _name.Equals(channel._name) &&
                   EqualityComparer<Dictionary<Guid, IUser>>.Default.Equals(admins, channel.admins) &&
                   EqualityComparer<Dictionary<Guid, IUser>>.Default.Equals(followers, channel.followers) &&
                   EqualityComparer<Dictionary<Guid, IMessage>>.Default.Equals(messages, channel.messages);
        }

        public override int GetHashCode()
        {
            var hashCode = -1020551394;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(_id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IUser>>.Default.GetHashCode(admins);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IUser>>.Default.GetHashCode(followers);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IMessage>>.Default.GetHashCode(messages);
            return hashCode;
        }
    }
}