using System;
using System.Runtime.Serialization;

namespace LeagueGram.Domain.Exception
{
  [Serializable]
  public class ChatNotFoundException : System.Exception
  {
    public ChatNotFoundException(Guid chatId)
      : base($"Chat with id {chatId} not found")
    {
    }

    public ChatNotFoundException()
    {
    }

    public ChatNotFoundException(string message) : base(message)
    {
    }

    public ChatNotFoundException(string message, System.Exception inner) : base(message, inner)
    {
    }

    protected ChatNotFoundException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
    }
  }
}