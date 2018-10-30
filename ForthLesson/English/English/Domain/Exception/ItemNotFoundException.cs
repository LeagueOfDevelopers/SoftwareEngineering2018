using System;
using System.Runtime.Serialization;

namespace English.Domain.Exception
{
    [Serializable]
    public class ItemNotFoundException : System.Exception
    {
        public ItemNotFoundException(Guid itemId)
            : base($"Item with id {itemId} not found")
        {
        }

        public ItemNotFoundException(Guid itemId, string nameOfClass)
            : base($"{nameOfClass} with id {itemId} not found")
        {
        }

        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string message) : base(message)
        {
        }

        public ItemNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected ItemNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
