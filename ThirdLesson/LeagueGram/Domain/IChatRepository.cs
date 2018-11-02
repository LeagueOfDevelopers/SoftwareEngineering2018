using System;

namespace LeagueGram.Domain
{
  public interface IChatRepository
  {
    IChat LoadChat(Guid chatId);
    void SaveChat(IChat chat);
  }
}