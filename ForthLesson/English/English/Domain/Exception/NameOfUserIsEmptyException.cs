using System;
using System.Runtime.Serialization;

namespace English.Domain.Exception
{
    [Serializable]
    public class NameOfUserIsEmptyException : System.Exception
    {
        public NameOfUserIsEmptyException(Guid itemId)
            : base($"Name of user is empty")
        {
        }

        public NameOfUserIsEmptyException()
        {
        }

        public NameOfUserIsEmptyException(string message) : base(message)
        {
        }

        public NameOfUserIsEmptyException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected NameOfUserIsEmptyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
