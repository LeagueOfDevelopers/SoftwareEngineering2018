using System;
using System.Runtime.Serialization;

namespace LeagueGram.Domain.Exception
{
  [Serializable]
  public class InsufficientRightsException : System.Exception
  {
    public InsufficientRightsException(Guid actorId, string operation)
      : base($"User {actorId} has insufficient rights to perform operation {operation}")
    {
    }

    public InsufficientRightsException()
    {
    }

    public InsufficientRightsException(string message) : base(message)
    {
    }

    public InsufficientRightsException(string message, System.Exception inner) : base(message, inner)
    {
    }

    protected InsufficientRightsException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
    }
  }
}