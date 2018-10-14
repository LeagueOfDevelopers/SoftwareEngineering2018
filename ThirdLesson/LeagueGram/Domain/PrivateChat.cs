using System;
using System.Collections.Generic;

namespace LeagueGram.Domain
{
  public class PrivateChat : Chat
  {
    public PrivateChat(Guid id, IEnumerable<Message> messages, ChatMember creator, ChatMember companion)
      : base(id, messages, new[] {creator, companion})
    {
    }

    protected override bool CanSendMessage(ChatMember chatMember)
    {
      return true;
    }

    protected override bool CanEditMessage(ChatMember chatMember, Message message)
    {
      return message.SenderId == chatMember.Id;
    }

    protected override bool CanDeleteMessage(ChatMember chatMember, Message message)
    {
      return message.SenderId == chatMember.Id;
    }
  }
}
