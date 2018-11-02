using System;

namespace LeagueGram.Application
{
  public interface IChatMessagingService
  {
    Guid SendMessage(Guid chatId, Guid senderId, string textMessage);

    void EditMessage(Guid chatId, Guid actorMemberId, Guid messageId, string newMessageText);

    void DeleteMessage(Guid chatId, Guid actorMemberId, Guid messageId);
  }
}