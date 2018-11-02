using System;
using System.Collections.Generic;

namespace LeagueGram.Domain
{
  public interface IChat
  {
    Guid Id { get; }
    IEnumerable<Message> Messages { get; }
    IEnumerable<ChatMember> Members { get; }
    Guid SendMessage(string messageText, Guid senderId);
    void EditMessage(Guid actorMemberId, Guid messageId, string newMessage);
    void DeleteMessage(Guid actorMemberId, Guid messageId);
  }
}