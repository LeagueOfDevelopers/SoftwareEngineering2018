using System;
using System.Collections.Generic;
using System.Linq;

namespace Messenger
{
    public class Dialogue : IChat
    {
        public Guid _id { get; }

        public IUser first_user { private set; get; }
        public IUser second_user { private set; get; }
        private Dictionary<Guid, IMessage> messages;

        public Dialogue(Guid id, IUser first_user, IUser second_user, Dictionary<Guid, IMessage> messages)
        {
            _id = id;
            this.first_user = first_user ?? throw new ArgumentNullException(nameof(first_user));
            this.second_user = second_user ?? throw new ArgumentNullException(nameof(second_user));
            this.messages = messages;
        }

        public void ChangeMessage(string changed_message, Guid message_id, Guid user_id)
        {
            if (messages.ContainsKey(message_id))
            {
                if (messages[message_id]._author._id == user_id)
                {
                    messages[message_id] = new Message(changed_message, messages[message_id]._id, messages[message_id]._author, messages[message_id]._departure_time);
                }
                else throw new InvalidOperationException($"This message not yours");
            }
            else throw new InvalidOperationException($"Message does not found");
        }
        public void RemoveMessage(Guid message_id, Guid user_id)
        {
            if (messages.ContainsKey(message_id))
            {
                if (messages[message_id]._author._id == user_id)
                {
                    messages.Remove(message_id);
                }
                else throw new InvalidOperationException($"This message not yours");
            }
            else throw new InvalidOperationException($"Message does not found");
        }
        public void SendMessage(IMessage message, Guid user_id)
        {
            if (first_user._id == user_id || second_user._id == user_id)
            {
                messages.Add(message._id, message);
            }
            else throw new InvalidOperationException($"This is not your dialogue");
            
        }

        public List<IMessage> GetMessages(IUser user)
        {
            if (first_user == user || second_user == user)
            {
                return messages.Values.ToList();
            }
            throw new InvalidOperationException($"You have not root to to this operation");
        }
        public IMessage GetLastMessages(IUser user)
        {
            var min_time = DateTimeOffset.MinValue;
            var empty_user = new User(string.Empty, Guid.Empty);
            var last_message = new Message(string.Empty, Guid.Empty, empty_user, min_time);

            if (first_user == user || second_user == user)
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
            throw new InvalidOperationException($"You have not root to to this operation");
        }

        public override bool Equals(object obj)
        {
            var dialogue = obj as Dialogue;
            return dialogue != null &&
                   _id.Equals(dialogue._id) &&
                   EqualityComparer<IUser>.Default.Equals(first_user, dialogue.first_user) &&
                   EqualityComparer<IUser>.Default.Equals(second_user, dialogue.second_user) &&
                   EqualityComparer<Dictionary<Guid, IMessage>>.Default.Equals(messages, dialogue.messages);
        }

        public override int GetHashCode()
        {
            var hashCode = -1217293218;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(_id);
            hashCode = hashCode * -1521134295 + EqualityComparer<IUser>.Default.GetHashCode(first_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<IUser>.Default.GetHashCode(second_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Guid, IMessage>>.Default.GetHashCode(messages);
            return hashCode;
        }
    }
}
