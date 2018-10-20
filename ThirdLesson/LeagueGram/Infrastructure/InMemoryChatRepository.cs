using System;
using System.Collections.Generic;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Infrastructure
{
  public class InMemoryChatRepository : InMemoryRepository<Chat>
  {    
  }
}