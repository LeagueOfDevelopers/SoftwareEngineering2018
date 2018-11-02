using System;

namespace LeagueGram.Domain
{
  public class Message
  {
    public Message(Guid messageId, string text, Guid senderId, DateTimeOffset sentOn)
    {
      MessageId = messageId;
      Text = text;
      SenderId = senderId;
      SentOn = sentOn;
    }

    public Guid MessageId { get; }

    public string Text { get; private set; }

    public Guid SenderId { get; }

    public DateTimeOffset SentOn { get; }

    public void Edit(string newText)
    {
      Text = newText;
    }
  }
}
