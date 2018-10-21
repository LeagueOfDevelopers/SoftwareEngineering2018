using System;
using System.Collections.Generic;

namespace Messenger
{
    public class Message : IMessage
    {
        public string _text;

        public Message(string text, Guid id, IUser author, DateTimeOffset departure_time)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _id = id;
            _author = author ?? throw new ArgumentNullException(nameof(author));
            _departure_time = departure_time;
        }

        public Guid _id { get; }
        public IUser _author { get; }
        public DateTimeOffset _departure_time { get; }

        public override bool Equals(object obj)
        {
            var message = obj as Message;
            return message != null &&
                   _text.Equals(message._text) &&
                   _id.Equals(message._id) &&
                   EqualityComparer<IUser>.Default.Equals(_author, message._author) &&
                   _departure_time.Equals(message._departure_time);
        }

        public override int GetHashCode()
        {
            var hashCode = -419153245;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_text);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(_id);
            hashCode = hashCode * -1521134295 + EqualityComparer<IUser>.Default.GetHashCode(_author);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(_departure_time);
            return hashCode;
        }
    }
}
