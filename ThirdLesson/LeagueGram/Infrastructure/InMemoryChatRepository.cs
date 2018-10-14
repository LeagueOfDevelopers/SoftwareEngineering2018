using System;
using System.Collections.Generic;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Infrastructure
{
  public class InMemoryChatRepository : IChatRepository
  {
    public IChat LoadChat(Guid chatId)
    {
      if (!_chats.ContainsKey(chatId))
      {
        throw new ChatNotFoundException(chatId);
      }

      return _chats[chatId];
    }

    public void SaveChat(IChat chat)
    {
      _chats[chat.Id] = chat;
    }

    private readonly Dictionary<Guid, IChat> _chats = new Dictionary<Guid, IChat>();
  }
}