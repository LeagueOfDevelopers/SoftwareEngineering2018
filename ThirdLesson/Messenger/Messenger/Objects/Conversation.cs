using System;
using System.Collections.Generic;
using System.Linq;

namespace Messenger
{
    public class Conversation : IChat
    {
        private Dictionary<Guid, IUser> interviewers;
        private Dictionary<Guid, IMessage> messages;

        public string _name;

        public Conversation(Dictionary<Guid, IUser> interviewers, Dictionary<Guid, IMessage> messages, string name, Guid id)
        {
            this.interviewers = interviewers ?? throw new ArgumentNullException(nameof(interviewers));
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _id = id;
        }

        public Guid _id { get; }

        public void AddNewUser(IUser user, IUser interviewer)
        {
            if (interviewers.ContainsKey(interviewer._id))
            {
                interviewers.Add(user._id, user);
            }
            else throw new InvalidOperationException($"Firstly join the conversation");

        }
        public void ChangeName(string new_name, Guid user_id)
        {
            if (interviewers.ContainsKey(user_id))
            {
                _name = new_name;
            }
            else throw new InvalidOperationException($"Firstly join the conversation");
        }

        public void ChangeMessage(string changed_message, Guid message_id, Guid user_id)
        {
            if (messages.TryGetValue(message_id, out IMessage finded_message) && interviewers.TryGetValue(user_id, out IUser finded_user))
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
            if (messages.TryGetValue(message_id, out IMessage finded_message) && interviewers.TryGetValue(user_id, out IUser finded_user))
            {
                if (finded_message._author == finded_user)
                {
                    messages.Remove(message_id);
                }
                else throw new InvalidOperationException($"This message not your");
            }
            else throw new InvalidOperationException($"No message or user does not exist");
        }
        public void SendMessage(IMessage message, Guid user_id)
        {
            if (interviewers.ContainsKey(user_id))
            {
                messages.Add(message._id, message);
            }
            else throw new InvalidOperationException($"Firstly join the conversation");
        }

        public List<IMessage> GetMessages(IUser user)
        {
            if (interviewers.ContainsKey(user._id))
            {
                return messages.Values.ToList();
            }
            throw new InvalidOperationException($"Firstly join the conversation");

        }

        public IMessage GetLastMessages(IUser user)
        {
            var min_time = DateTimeOffset.MinValue;
            var empty_user = new User(string.Empty, Guid.Empty);
            var last_message = new Message(string.Empty, Guid.Empty, empty_user, min_time);

            if (interviewers.ContainsKey(user._id))
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
            throw new InvalidOperationException($"Firstly join the conversation");
        }

        public override bool Equals(object obj)
        {
            var conversation = obj as Conversation;
            return conversation != null &&
                   EqualityComparer<Dictionary<Guid, IUser>>.Default.Equals(interviewers, conversation.interviewers) &&
                   EqualityComparer<Dictionary<Guid, IMessage>>.Default.Equals(messages, conversation.messages) &&
                   _name.Equals(conversation._name) &&
                   _id.Equals(conversation._id);
        }

        public override int GetHashCode()
        {
            var hashCode = -2106479715;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IUser>>.Default.GetHashCode(interviewers);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IMessage>>.Default.GetHashCode(messages);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(_id);
            return hashCode;
        }
    }
}
