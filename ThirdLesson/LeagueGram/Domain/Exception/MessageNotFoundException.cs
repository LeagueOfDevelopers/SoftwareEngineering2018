using System;
using System.Runtime.Serialization;

namespace LeagueGram.Domain.Exception
{
  [Serializable]
  public class MessageNotFoundException : System.Exception
  {
    public MessageNotFoundException(Guid chatId, Guid messageId)
      : base($"Message {messageId} not found in chat {chatId}")
    {
    }

    public MessageNotFoundException()
    {
    }

    public MessageNotFoundException(string message) : base(message)
    {
    }

    public MessageNotFoundException(string message, System.Exception inner) : base(message, inner)
    {
    }

    protected MessageNotFoundException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
    }
  }
}