using System;
using System.Collections.Generic;

namespace Messenger
{
    public class User : IUser
    {
        public User(string nickname, Guid id)
        {
            this.nickname = nickname ?? throw new ArgumentNullException(nameof(nickname));
            _id = id;
        }

        public string nickname { private set; get; }
        public Guid _id { get; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   nickname.Equals(user.nickname) &&
                   _id.Equals(user._id);
        }

        public override int GetHashCode()
        {
            var hashCode = -647874936;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nickname);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(_id);
            return hashCode;
        }
    }
}
