using System;
using System.Runtime.Serialization;

namespace LeagueGram.Domain.Exception
{
  [Serializable]
  public class UserNotFoundException : System.Exception
  {
    public UserNotFoundException(Guid userId) : base($"User with id {userId} not found")
    {
    }

    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, System.Exception inner) : base(message, inner)
    {
    }

    protected UserNotFoundException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
    }
  }
}