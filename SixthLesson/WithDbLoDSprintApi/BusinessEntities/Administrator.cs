using System;

namespace BusinessEntities
{
    public class Administrator
    {
        public Administrator(Guid id, string nickName)
        {
            Id = id;
            NickName = nickName ?? throw new ArgumentNullException(nameof(nickName));
        }

        public Guid Id { get; }

        public string NickName { get; }
    }
}
